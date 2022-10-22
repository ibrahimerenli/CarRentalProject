using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Linq;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Geçerli Değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            if (_validatorType.BaseType != null)
            {
                var entityType = _validatorType.BaseType.GetGenericArguments()[0];
                var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
                foreach (var entity in entities)
                {
                    ValidationTool.Validate(validator, entity);
                }
            }
        }
    }
}
