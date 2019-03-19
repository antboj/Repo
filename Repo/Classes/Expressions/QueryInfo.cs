using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Repo.Classes.Expressions
{
    public class QueryInfo
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<SortInfo> Sorters { get; set; } = new List<SortInfo>();
        public FilterInfo Filter { get; set; }

        // Where Ekspresija
        public Expression<Func<TEntity, bool>> GetWherExpression<TEntity>(string operatorValue,
            string propertyName, string searchValue)
        {
            // Parametar : x =>
            ParameterExpression parameterEx = Expression.Parameter(typeof(TEntity), "x");

            // Property po kojem se pretrazuje : x.Property
            var propertyEx = Expression.Property(parameterEx, propertyName);

            // Tip propertija po kojem se pretrazuje
            var type = propertyEx.Type;

            // Konvertovanje
            var convertedTypeValue = Convert.ChangeType(searchValue, type);

            // Konstanta, vrijednost po kojoj se poredi
            var constantEx = Expression.Constant(convertedTypeValue);

            BinaryExpression binaryEx;

            switch (convertedTypeValue)
            {
                case string _:
                    binaryEx = GetBinaryExpressionForString(operatorValue, propertyEx, constantEx);
                    break;
                case int _:
                    binaryEx = GetBinaryExpressionForInt(operatorValue, propertyEx, constantEx);
                    break;
                default:
                    throw new ArgumentException();
            }

            return Expression.Lambda<Func<TEntity, bool>>(binaryEx, parameterEx);
        }

        // Binarna int ekspresija za Where
        private static BinaryExpression GetBinaryExpressionForInt(string operatorValue, MemberExpression propertyEx, ConstantExpression constantEx)
        {
            switch (operatorValue)
            {
                case "GT":
                    return Expression.GreaterThan(propertyEx, constantEx);
                case "LT":
                    return Expression.LessThan(propertyEx, constantEx);
                case "EQ":
                    return Expression.Equal(propertyEx, constantEx);
                default:
                    throw new InvalidOperationException();
            }
        }

        // Binarna string ekspresija za Where
        private static BinaryExpression GetBinaryExpressionForString(string operatorValue, MemberExpression propertyEx, ConstantExpression constantEx)
        {
            //var trueExpression = Expression.Constant(true, typeof(bool));
            //BinaryExpression bin;

            switch (operatorValue)
            {
                case "EQ":
                    return Expression.Equal(propertyEx, constantEx);
                default:
                    throw new InvalidOperationException();
            }
        }

        // OrderBy Ekspresija
        public Expression<Func<TEntity, object>> GetOrderByExpression<TEntity>(string propertyName)
        {
            ParameterExpression parameterEx = Expression.Parameter(typeof(TEntity), "x");
            var propertyEx = Expression.Property(parameterEx, propertyName);
            var convertEx = Expression.Convert(propertyEx, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(convertEx, parameterEx);
        }

        //public Expression<Func<TEntity, TKey>> GetOrderByDescendingExpression<TEntity, TKey>(string propertyName)
        //{
        //    ParameterExpression parameterEx = Expression.Parameter(typeof(TEntity), "x");
        //    var propertyEx = Expression.Property(parameterEx, propertyName);
        //    return Expression.Lambda<Func<TEntity, TKey>>(propertyEx, parameterEx);
        //}
    }
}
