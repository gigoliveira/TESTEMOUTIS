using System;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Value object for CustomerSnapshot. It holds customer details such as ExternalCustomerId and CustomerName.
    /// </summary>
    public class CustomerSnapshot
    {
        /// <summary>
        /// Gets the external identifier of the customer.
        /// </summary>
        public Guid ExternalCustomerId { get; private set; }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Default constructor for CustomerSnapshot.
        /// </summary>
        public CustomerSnapshot()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerSnapshot"/> class.
        /// </summary>
        /// <param name="externalCustomerId">The external customer ID.</param>
        /// <param name="customerName">The customer name.</param>
        public CustomerSnapshot(Guid externalCustomerId, string customerName)
        {
            ExternalCustomerId = externalCustomerId;
            Name = customerName;
        }

        /// <summary>
        /// Factory method to create and validate a CustomerSnapshot instance.
        /// Throws validation errors if any validation rule is violated.
        /// </summary>
        /// <param name="externalCustomerId">The external customer ID.</param>
        /// <param name="customerName">The customer name.</param>
        /// <returns>A valid CustomerSnapshot instance.</returns>
        public static CustomerSnapshot Create(Guid externalCustomerId, string customerName)
        {
            var customerSnapshot = new CustomerSnapshot(externalCustomerId, customerName);
            customerSnapshot.Validate(); // Validates using the specified rules

            return customerSnapshot;
        }

        /// <summary>
        /// Validates the CustomerSnapshot object.
        /// Uses the CustomerSnapshotValidator to check if the customer data is valid.
        /// </summary>
        /// <returns>A validation result containing errors, if any.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CustomerSnapshotValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
