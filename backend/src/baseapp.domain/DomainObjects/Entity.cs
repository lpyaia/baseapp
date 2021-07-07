using FluentValidation;
using FluentValidation.Results;
using System;

namespace BaseApp.Domain.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public bool IsValid => ValidationResult != null && ValidationResult.IsValid;

        public ValidationResult? ValidationResult { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public bool Validate<T>(T entity, AbstractValidator<T> validator)
        {
            ValidationResult = validator.Validate(entity);

            return ValidationResult.IsValid;
        }
    }
}