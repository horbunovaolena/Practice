using System;
using Xunit;

namespace Task1_HandmadeWorkshop.Tests
{
    public class ClothingOrderTests
    {
        [Fact]
        public void CalculateFinalPrice_CottonStandartDuration_ShouldReturnBasePrice()
        {
            // Arrange
            var order = new ClothingOrder("Бавовна", "Гуцульщина", 10, 1000m, "S");
            decimal expectedPrice = 1000m; // Base price for cotton with standard duration
            // Act
            decimal actualPrice = order.CalculateFinalPrice();
            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void CalculateFinalPrice_LinenStandartDuration_ShouldReturnBasePriceWithPremiumMarkup()
        {
            // Arrange
            var order = new ClothingOrder("Льон", "Полтава", 10, 1000m, "M");
            decimal expectedPrice = 1100m; // Base price + 10% for premium material
            // Act
            decimal actualPrice = order.CalculateFinalPrice();
            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void CalculateFinalPrice_SilkStandartDuration_ShouldReturnBasePriceWithPremiumMarkup()
        {
            // Arrange
            var order = new ClothingOrder("Шовк", "Сучасний", 10, 1000m, "L");
            decimal expectedPrice = 1100m; // Base price + 10% for premium material
            // Act
            decimal actualPrice = order.CalculateFinalPrice();
            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void CalculateFinalPrice_CottonUrgent_ShouldReturnBasePriceWithUrgentMarkup()
        {
            // Arrange
            var order = new ClothingOrder("Бавовна", "Гуцульщина", 4, 1000m, "S");
            decimal expectedPrice = 1200m; // Base price + 20% for urgent order

            // Act
            decimal actualPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }
        [Fact]
        public void CalculateFinalPrice_LinenUrgent_ShouldReturnBasePriceWithPremiumAndUrgentMarkup()
        {
            // Arrange
            var order = new ClothingOrder("Льон", "Полтава", 4, 1000m, "M");
            decimal expectedPrice = 1320m; // Base price + 10% for premium material + 20% for urgent order
            // Act
            decimal actualPrice = order.CalculateFinalPrice();
            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }
        [Fact]
        public void CalculateFinalPrice_SilkUrgent_ShouldReturnBasePriceWithPremiumAndUrgentMarkup()
        {
            // Arrange
            var order = new ClothingOrder("Шовк", "Сучасний", 4, 1000m, "L");
            decimal expectedPrice = 1320m; // Base price + 10% for premium material + 20% for urgent order
            // Act
            decimal actualPrice = order.CalculateFinalPrice();
            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Theory]                                                    //Тести логіки Pattern Matching (Technique & Category)
        [InlineData("Полтава", "Вишивка біллю (білим по білому)")]
        [InlineData("Гуцульщина", "Низинка або кучерявий шов")]
        [InlineData("Сучасний", "Машинна вишивка або принт")]
        [InlineData("Kyiv", "Техніка обирається майстром")] // Дефолтний випадок (_)
        public void Technique_ShouldReturnCorrectValue_BasedOnRegion(string region, string expectedTechnique)
        {
            var order = new ClothingOrder("Бавовна", region, 7, 1000m, "L");
                        
            // Act & Assert
            Assert.Equal(expectedTechnique, order.Technique);
        }

        [Theory]
        [InlineData("Льон", "Автентичний одяг (старовинний стиль)")]
        [InlineData("Шовк", "Святковий одяг")]
        [InlineData("Бавовна", "Повсякденний текстильний виріб")]
        [InlineData("Вовна", "Сучасний текстильний виріб")]
        public void Category_ShouldReturnCorrectValue_BasedOnMaterial(string material, string expectedCategory)
        {
            // Arrange
            var order = new ClothingOrder(material, "Сучасний", 7, 1000m, "M");

            // Act & Assert
            Assert.Equal(expectedCategory, order.Category);
        }
        [Fact]
        public void ToString_ShouldContainClothingSpecificData()
        {
            // Arrange
            var order = new ClothingOrder("Льон", "Полтава", 7, 1000m, "XL");

            // Act
            string result = order.ToString();

            // Assert
            Assert.Contains("Тип:          Одяг", result);
            Assert.Contains("Розмір: XL", result);
            Assert.Contains("Техніка:      Вишивка біллю (білим по білому)", result);
        }

        [Fact]
        public void GetLabel_ShouldReturnValidFormat()
        {
            // Arrange
            var order = new ClothingOrder("Шовк", "Гуцульщина", 10, 2000m, "S");
            // Очікувана ціна: 2000 * 1.1 = 2200.00
            string expectedFormattedPrice = 2200m.ToString("C");

            // Act
            string label = order.GetLabel();

            // Assert
            // Перевіряємо структуру рядка, яку вимагає інтерфейс IExportable
            Assert.StartsWith("[ОДЯГ] Шовк | Гуцульщина | Розмір: S |", label);
            Assert.EndsWith(expectedFormattedPrice, label);
        }

        [Fact]
        public void Constructor_ShouldInitializeAllProperties()
        {
            // Arrange
            const decimal basePrice = 1500m;
            const int days = 7;
            string material = "Бавовна";
            string region = "Гуцульщина";
            string size = "XL";

            // Act
            var order = new ClothingOrder(material, region, days, basePrice, size);

            // Assert
            Assert.Equal(material, order.Material);
            Assert.Equal(region, order.Region);
            Assert.Equal(days, order.Days);
            Assert.Equal(basePrice, order.BasePrice);
            Assert.Equal(size, order.Size);
        }

        [Fact]
        public void CalculateFinalPrice_EdgeCaseUrgentThreshold_ShouldNotApplyUrgentMultiplier()
        {
            // Arrange - Days exactly at threshold (5 days should not be urgent)
            var order = new ClothingOrder("Бавовна", "Гуцульщина", 5, 1000m, "M");
            decimal expectedPrice = 1000m; // No urgent multiplier at threshold

            // Act
            decimal actualPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void CalculateFinalPrice_DaysBelowThreshold_ShouldApplyUrgentMultiplier()
        {
            // Arrange - Days = 1 should be urgent
            var order = new ClothingOrder("Бавовна", "Гуцульщина", 1, 1000m, "M");
            decimal expectedPrice = 1200m; // Base price + 20% urgent

            // Act
            decimal actualPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Theory]
        [InlineData("Бавовна", "Повсякденний текстильний виріб")]
        [InlineData("Льон", "Автентичний одяг (старовинний стиль)")]
        [InlineData("Шовк", "Святковий одяг")]
        [InlineData("Вовна", "Сучасний текстильний виріб")]
        [InlineData("Невідомий матеріал", "Сучасний текстильний виріб")] // Default case
        public void Category_ShouldReturnCorrectValue_BasedOnMaterial_Extended(string material, string expectedCategory)
        {
            // Arrange
            var order = new ClothingOrder(material, "Сучасний", 7, 1000m, "M");

            // Act & Assert
            Assert.Equal(expectedCategory, order.Category);
        }

        [Fact]
        public void ToString_ShouldContainAllClothingOrderData()
        {
            // Arrange
            var order = new ClothingOrder("Шовк", "Полтава", 10, 2000m, "XXL");

            // Act
            string result = order.ToString();

            // Assert
            Assert.Contains("ВАШЕ ЗАМОВЛЕННЯ", result);
            Assert.Contains("Тип:          Одяг", result);
            Assert.Contains("Матеріал:     Шовк", result);
            Assert.Contains("Регіон:       Полтава", result);
            Assert.Contains("Техніка:      Вишивка біллю (білим по білому)", result);
            Assert.Contains("Категорія:    Святковий одяг", result);
            Assert.Contains("Термін:       10 днів", result);
            Assert.Contains("Розмір: XXL", result);
        }

        [Fact]
        public void GetLabel_ShouldIncludeAllRequiredInformation()
        {
            // Arrange
            var order = new ClothingOrder("Бавовна", "Сучасний", 7, 3000m, "M");

            // Act
            string label = order.GetLabel();

            // Assert
            Assert.Contains("[ОДЯГ]", label);
            Assert.Contains("Бавовна", label);
            Assert.Contains("Сучасний", label);
            Assert.Contains("Розмір: M", label);
        }

        [Fact]
        public void CalculateFinalPrice_VariousScenarios_ShouldCalculateCorrectly()
        {
            // Test scenario 1: Cotton with standard duration
            var order1 = new ClothingOrder("Бавовна", "Гуцульщина", 10, 1000m, "XS");
            decimal finalPrice1 = order1.CalculateFinalPrice();
            Assert.True(finalPrice1 >= 1000m, "Final price should be at least the base price");

            // Test scenario 2: Linen with urgent
            var order2 = new ClothingOrder("Льон", "Полтава", 4, 2000m, "S");
            decimal finalPrice2 = order2.CalculateFinalPrice();
            Assert.True(finalPrice2 >= 2000m, "Final price should be at least the base price");

            // Test scenario 3: Silk with urgent
            var order3 = new ClothingOrder("Шовк", "Сучасний", 2, 5000m, "L");
            decimal finalPrice3 = order3.CalculateFinalPrice();
            Assert.True(finalPrice3 >= 5000m, "Final price should be at least the base price");
        }

        [Fact]
        public void CalculateFinalPrice_AllPremiumAndUrgent_MaxPriceMultiplier()
        {
            // Arrange - Maximum multiplier scenario: premium material + urgent
            var order = new ClothingOrder("Шовк", "любой регион", 1, 1000m, "L");
            // Expected: 1000 * 1.1 * 1.2 = 1320

            // Act
            decimal finalPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(1320m, finalPrice);
        }
    }
}
