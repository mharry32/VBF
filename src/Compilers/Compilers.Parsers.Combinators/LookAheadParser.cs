﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VBF.Compilers.Scanners;

namespace VBF.Compilers.Parsers.Combinators
{
    public class ChooseBestParser<T> : Parser<T>
    {
        public Parser<T> Parser1 { get; private set; }
        public Parser<T> Parser2 { get; private set; }

        public ChooseBestParser(Parser<T> parser1, Parser<T> parser2)
        {
            Parser1 = parser1;
            Parser2 = parser2;
        }

        public override Func<Scanners.ForkableScanner, ParserContext, Result<TFuture>> Run<TFuture>(Future<T, TFuture> future)
        {
            return (scanner, context) =>
            {
                ForkableScanner s1 = scanner;
                ForkableScanner s2 = scanner;
                return context.ChooseBest(Parser1.Run(future)(s1, context), Parser2.Run(future)(s2, context));
            };
        }
    }
}