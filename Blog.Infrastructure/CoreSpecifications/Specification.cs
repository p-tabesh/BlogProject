using System.Linq.Expressions;

namespace Core.Repository.Model.Specifications
{
    public abstract class Specification<T>
    {
        // NOTE: This might need improvement. Processing of specifications (in this case using Linq) is related to
        // implementation of Infrastructure and should be separated. This implementation will face problem
        // if repository infrastructure modified to one that does not support Linq.

        public abstract Expression<Func<T, bool>> Expression { get; }

        protected virtual bool IsApplicable => true;

        public bool IsSatisfiedBy(T entity)
        {
            return !IsApplicable || Expression.Compile()(entity);
        }

        public Specification<T> And(Specification<T> specification)
        {
            if (!IsApplicable)
            {
                // Call recursively the same method by replacing "true" 
                return True().And(specification);
            }

            return specification.IsApplicable ? new AndSpecification<T>(this, specification) : this;
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (!IsApplicable)
            {
                // Call recursively the same method by replacing "false"
                return False().Or(specification);
            }

            return specification.IsApplicable ? new OrSpecification<T>(this, specification) : this;
        }

        public static Specification<T> True()
        {
            return new TrueSpecification<T>();
        }

        public static Specification<T> False()
        {
            return new FalseSpecification<T>();
        }
    }
}
