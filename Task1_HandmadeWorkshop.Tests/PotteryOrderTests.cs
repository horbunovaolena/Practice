using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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

        [Fact]
        public void GetLabel_ShouldReturnValidFormat()
        {
            // Arrange
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 10, 1500m, 2.5m);
            // Expected price: 1500 + 300 = 1800
            string expectedFormattedPrice = 1800m.ToString("C");

            // Act
            string label = potteryOrder.GetLabel();

            // Assert
            Assert.StartsWith("[ПОСУД] Глина | Полтава | Об'єм:", label);
            Assert.Contains("л", label);
            Assert.EndsWith(expectedFormattedPrice, label);
        }

        [Fact]
        public void CalculateFinalPrice_VolumeIsNotBiggerThan2Liters_ShouldNotAdd300()
        {
            // Arrange
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 10, basePrice, 2m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice, finalPrice); // No 300 added since volume is exactly 2
        }

        [Fact]
        public void CalculateFinalPrice_VolumeLessThan2Liters_ShouldNotAdd300()
        {
            // Arrange
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 10, basePrice, 1.5m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice, finalPrice); // No 300 added since volume is less than 2
        }

        [Fact]
        public void CalculateFinalPrice_EdgeCaseUrgentThreshold_ShouldApplyUrgentMultiplier()
        {
            // Arrange - Days exactly at threshold (5 days should not be urgent)
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 5, basePrice, 1m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice, finalPrice); // Days = 5 is not < 5, so no urgent multiplier
        }

        [Fact]
        public void CalculateFinalPrice_DaysBelowThreshold_ShouldApplyUrgentMultiplier()
        {
            // Arrange - Days = 1 should be urgent
            const decimal basePrice = 1000m;
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 1, basePrice, 1m);

            // Act
            var finalPrice = potteryOrder.CalculateFinalPrice();

            // Assert
            Assert.Equal(basePrice * 1.2m, finalPrice);
        }

        [Theory]
        [InlineData("Глина")]
        [InlineData("Фаянс")]
        [InlineData("Порцеляна")]
        [InlineData("Невідомий матеріал")]
        public void Category_ShouldReturnCorrectValue_BasedOnMaterial(string material)
        {
            // Arrange
            var potteryOrder = new PotteryOrder(material, "Полтава", 7, 1000m, 1.5m);

            // Act
            var category = potteryOrder.Category;

            // Assert
            Assert.NotNull(category);
            Assert.NotEmpty(category);
        }

        [Theory]
        [InlineData("Полтава", "Розпис у полтавському стилі")]
        [InlineData("Гуцульщина", "Гуцульська кераміка (орнамент)")]
        [InlineData("Сучасний", "Глазурування та обпалення")]
        [InlineData("Київ", "Техніка обирається майстром")] // Default case
        public void Technique_ShouldReturnCorrectValue_BasedOnRegion(string region, string expectedTechnique)
        {
            // Arrange
            var potteryOrder = new PotteryOrder("Глина", region, 7, 1000m, 1.5m);

            // Act & Assert
            Assert.Equal(expectedTechnique, potteryOrder.Technique);
        }

        [Fact]
        public void ToString_ShouldContainPotterySpecificData()
        {
            // Arrange
            var potteryOrder = new PotteryOrder("Глина", "Полтава", 7, 1000m, 3.0m);

            // Act
            string result = potteryOrder.ToString();

            // Assert
            Assert.Contains("Тип:          Посуд", result);
            Assert.Contains("Об'єм:", result);
            Assert.Contains("л", result);
            Assert.Contains("Матеріал:     Глина", result);
        }

        [Fact]
        public void Constructor_ShouldInitializeAllProperties()
        {
            // Arrange
            const decimal basePrice = 2000m;
            const decimal volume = 3.5m;
            const int days = 10;
            string material = "Фаянс";
            string region = "Сучасний";

            // Act
            var potteryOrder = new PotteryOrder(material, region, days, basePrice, volume);

            // Assert
            Assert.Equal(material, potteryOrder.Material);
            Assert.Equal(region, potteryOrder.Region);
            Assert.Equal(days, potteryOrder.Days);
            Assert.Equal(basePrice, potteryOrder.BasePrice);
            Assert.Equal(volume, potteryOrder.Volume);
        }
    }
}
