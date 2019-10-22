#pragma warning disable EPS02 // Non-readonly struct used as in-parameter

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Superalgos.IntelliToken.IntelliErc20Token
{
    public static class IntelliIntegralConverterExtensions
    {
        public const byte TOKEN_DECIMALS = 18;
        public const long MEGA_FACTOR = 1000000;

        public static readonly BigInteger TokenFactor = BigInteger.Pow(10, TOKEN_DECIMALS);

        public static BigInteger Intelli(this in byte value) => value * TokenFactor;
        public static BigInteger Intelli(this in sbyte value) => value * TokenFactor;
        public static BigInteger Intelli(this in short value) => value * TokenFactor;
        public static BigInteger Intelli(this in ushort value) => value * TokenFactor;
        public static BigInteger Intelli(this in int value) => value * TokenFactor;
        public static BigInteger Intelli(this in uint value) => value * TokenFactor;
        public static BigInteger Intelli(this in long value) => value * TokenFactor;
        public static BigInteger Intelli(this in ulong value) => value * TokenFactor;

        public static BigInteger MIntelli(this in byte value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in sbyte value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in short value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in ushort value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in int value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in uint value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in long value) => value * TokenFactor * MEGA_FACTOR;
        public static BigInteger MIntelli(this in ulong value) => value * TokenFactor * MEGA_FACTOR;
    }
}
