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
        
        public BinaryExpression GetBinaryExpression(ParameterExpression parameterEx, string operatorValue,
            string propertyName, string searchValue)
        {

            Expression propertyEx = parameterEx;
            Expression currentParameter = parameterEx;
            var currentType = parameterEx.Type;

            string[] allProperties = propertyName.Split(".");
            foreach (string currentProperty in allProperties)
            {
                // RIP
                //propertyEx = Expression.Property(propertyEx, currentProperty);

                if (!DoesPropertyExist(currentProperty, currentType))
                {
                    throw new Exception($"Property {propertyName} ne postoji u tipu {currentType.Name}");
                }

                currentParameter = Expression.Property(currentParameter, currentProperty);
                currentType = currentParameter.Type;
                propertyEx = Expression.Property(propertyEx, currentProperty);
            }
            
            var type = propertyEx.Type;
            var convertedTypeValue = Convert.ChangeType(searchValue, type);
            var constantEx = Expression.Constant(convertedTypeValue);

            // x.Property == const
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

            //Expression<Func<TEntity, bool>> trueExp = x => true;
            //Expression result = trueExp.Body;
            // for ...
            //result = Expression.AndAlso(result, binaryEx); // &&
            //bE = Expression.And(bE, binaryEx);

            return binaryEx;
        }

        public Expression<Func<TEntity, bool>> GetWhere<TEntity>(Expression binary, ParameterExpression parameterEx)
        {
            return Expression.Lambda<Func<TEntity, bool>>(binary, parameterEx);
        }
        

        // Binarna int ekspresija
        private static BinaryExpression GetBinaryExpressionForInt(string operatorValue, Expression propertyEx, ConstantExpression constantEx)
        {
            switch (operatorValue)
            {
                case "gt":
                    return Expression.GreaterThan(propertyEx, constantEx);
                case "lt":
                    return Expression.LessThan(propertyEx, constantEx);
                case "eq":
                    return Expression.Equal(propertyEx, constantEx);
                case "loe":
                    return Expression.LessThanOrEqual(propertyEx, constantEx);
                case "goe":
                    return Expression.GreaterThanOrEqual(propertyEx, constantEx);
                default:
                    throw new InvalidOperationException();
            }
        }

        // Binarna string ekspresija
        private static BinaryExpression GetBinaryExpressionForString(string operatorValue, Expression propertyEx, ConstantExpression constantEx)
        {
            //var trueExpression = Expression.Constant(true, typeof(bool));
            //BinaryExpression bin;

            switch (operatorValue)
            {
                case "eq":
                    return Expression.Equal(propertyEx, constantEx);
                default:
                    throw new InvalidOperationException();
            }
        }

        // OrderBy Ekspresija
        public Expression<Func<TEntity, object>> GetOrderByExpression<TEntity>(string propertyName)
        {
            ParameterExpression parameterEx = Expression.Parameter(typeof(TEntity), "x");

            Expression currentParameter = parameterEx;

            var currentType = parameterEx.Type;
            
            string[] allProperties = propertyName.Split(".");
            foreach (string currentProperty in allProperties)
            {
                if (!DoesPropertyExist(currentProperty, currentType))
                {
                    throw new Exception($"Property {propertyName} ne postoji u tipu {currentType.Name}");
                }

                currentParameter = Expression.Property(currentParameter, currentProperty);
                currentType = currentParameter.Type;
            }
            
            var convertEx = Expression.Convert(currentParameter, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(convertEx, parameterEx);
        }

        private bool DoesPropertyExist(string currentProperty, Type currentType)
        {
            PropertyInfo[] allPropertiesInfo = currentType.GetProperties();
            foreach (var curP in allPropertiesInfo)
            {
                if (curP.Name == currentProperty)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
