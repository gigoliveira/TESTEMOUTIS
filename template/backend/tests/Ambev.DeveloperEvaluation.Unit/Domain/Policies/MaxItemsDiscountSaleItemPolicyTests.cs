using System;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Policies;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Policies
{
    public class MaxItemsDiscountSaleItemPolicyTests
    {
        [Fact]
        public void Given_ItemWithQuantityAbove20_When_Validated_Then_ShouldThrowException()
        {
            var product = new ProductSnapshot(Guid.NewGuid(), "Beer", 10m);
            var item = new SaleItem(product, 21);
            var policy = new MaxItemsDiscountSaleItemPolicy();

            Assert.Throws<InvalidOperationException>(() => policy.ApplyDiscount(item));
        }

        [Fact]
        public void Given_ItemWithQuantityBetween10And20_When_Validated_Then_ShouldApply20PercentDiscount()
        {
            var product = new ProductSnapshot(Guid.NewGuid(), "Beer", 10m);
            var item = new SaleItem(product, 15);
            var policy = new MaxItemsDiscountSaleItemPolicy();

            var discount = policy.ApplyDiscount(item);

            Assert.Equal(item.TotalValue * 0.20m, discount);
        }

        [Fact]
        public void Given_ItemWithQuantityBetween4And9_When_Validated_Then_ShouldApply10PercentDiscount()
        {
            var product = new ProductSnapshot(Guid.NewGuid(), "Beer", 10m);
            var item = new SaleItem(product, 5);
            var policy = new MaxItemsDiscountSaleItemPolicy();

            var discount = policy.ApplyDiscount(item);

            Assert.Equal(item.TotalValue * 0.10m, discount);
        }

        [Fact]
        public void Given_ItemWithQuantityBelow4_When_Validated_Then_ShouldNotApplyDiscount()
        {
            var product = new ProductSnapshot(Guid.NewGuid(), "Beer", 10m);
            var item = new SaleItem(product, 2);
            var policy = new MaxItemsDiscountSaleItemPolicy();

            var discount = policy.ApplyDiscount(item);

            Assert.Equal(0m, discount);
        }
    }
}
