pragma solidity 0.4.24;

import "openzeppelin-solidity/contracts/token/ERC20/IERC20.sol";
import "openzeppelin-solidity/contracts/token/ERC20/SafeERC20.sol";

import "./IIntelliMiner.sol";
import "./IntelliCommon.sol";
import "./ERC20TokenHolder.sol";
import "./IntelliCoreTeamRole.sol";

contract IntelliPool is IntelliCommon, ERC20TokenHolder, IntelliCoreTeamRole {
    using SafeERC20 for IERC20;

    enum PoolType {
        MinerPool,
        ReferralPool
    }

    PoolType private _poolType;
    mapping(address => bool) private _fundedMiners;

    constructor(PoolType poolType, address tokenAddress)
        ERC20TokenHolder(tokenAddress)
        IntelliCoreTeamRole()
        public {
        _poolType = poolType;
    }

    function transferToMiner(address minerAddress) public notTerminated onlyCoreTeam {
        require(!_fundedMiners[minerAddress]);

        IIntelliMiner intelliMiner = IIntelliMiner(minerAddress);
        
        require(intelliMiner.isIntelliMiner());
        
        uint8 minerCategory = intelliMiner.getCategory();

        require(minerCategory >= 0 && minerCategory <= 5);

        uint256 value = getCapacityByCategory(minerCategory);

        // For referral pools only transfer the 10% of the category capacity.
        if(_poolType == PoolType.ReferralPool) {
            value = value * 10 / 100;
        }

        _fundedMiners[minerAddress] = true;
        _token.safeTransfer(minerAddress, value);
    }

    function terminate() public onlyCoreTeam {
        _terminate();
    }
}
