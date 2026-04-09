using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_HandmadeWorkshop.Tests
{
    public class PotteryOrderTests
    {
        [Fact]
        public void CalculateFinalPrice_VolumeIsBiggerThan2Liters_PriceMustBe300More()
        {
            // Arrange
            const decimal basePrice = 1500m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 6, basePrice, 2.5m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(1800m, finalPrice);
        }

        [Fact]
        public void CalculateFinalPrice_DaysIsLessThanUrgent_PriceMustBeMultipliedByTheUrgentPriceMuktiplier()
        {
            // Arrange
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 2, basePrice, 1m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice * 1.2m, finalPrice);
        }
    }
}
