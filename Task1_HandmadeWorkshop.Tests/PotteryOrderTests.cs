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
        public void CalculateFinalPrice_DaysIsLessThanUrgent_PriceMustBeMultipliedByTheUrgentPriceMultiplier()
        {
            // Arrange
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 2, basePrice, 1m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice * 1.2m, finalPrice);
        }

        // TODO: додайте тест, який перевіряє, що якщо об'єм більше 2 літрів і термін менше 5 днів, то ціна враховує обидві умови (додає 300 грн і множить на 1.2)

        [Fact]
        public void CalculateFinalPrice_VolumeIsBiggerThan2LitersAndDaysIsLessThanUrgent_PriceMustBeAdjusted()
        {
            // Arrange
            const decimal basePrice = 1500m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 4, basePrice, 2.5m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal((basePrice + 300m) * 1.2m, finalPrice);
        }


        // TODO: Додай тест на перевірку 'ToString()'

        [Fact]
        public void ToString_ReturnsStringWithVolumeInfo()
        {
                // Arrange
                const decimal basePrice = 1500m;
                var potteryOrder = new PotteryOrder("Глина", "Полтава", 6, basePrice, 2.5m);

                // Act
                var result = potteryOrder.ToString();

                // Assert
                Assert.Contains("Об'єм", result);
                Assert.Contains("2,5", result);
                Assert.Contains("л", result);
        }
    }
}
