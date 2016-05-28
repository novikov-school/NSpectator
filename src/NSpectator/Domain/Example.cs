using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NSpectator.Domain.Extensions;

namespace NSpectator.Domain
{
    public class Example : ExampleBase
    {
        public override void Run(Spec spec)
        {
            if (IsAsync)
            {
                throw new ArgumentException("'It[]' cannot be set to an async delegate, please use 'ItAsync[]' instead");
            }

            action();
        }

        public override bool IsAsync => action.IsAsync();

        public Example(Expression<Action> expr, bool pending = false)
            : this(Parse(expr), null, expr.Compile(), pending) {}

        public Example(string name = "", string tags = "", Action action = null, bool pending = false)
            : base(name, tags, pending)
        {
            this.action = action;
        }

        Action action;
    }
}