using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PluralSightProcessor.Domain
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged
            = delegate { };

        protected void OnPropertyChanged(
            Expression<Func<object>> expression)
        {
            string propertyName = GetMemberName(expression.Body);
            this.PropertyChanged(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

       protected  string GetMemberName(
       Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType ==
                    ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression)
                        + "."
                        + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format(
                "Could not determine member from {0}",
                expression));
        }
    }
}
