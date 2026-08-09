using System;
using Xunit;

namespace Task1_HandmadeWorkshop.Tests
{
    public class ClothingOrderTests
    {
        [Theory]                               // тестуємо ціну для одягу, враховуючи матеріал,не терміново (>5днів)
        [InlineData("Бавовна", 6, 1000, 1000)]  //звичайний матеріал, не терміново
        [InlineData("Льон", 6, 1100, 1100)]    // Преміум матеріал (+10%), не терміново
        [InlineData("Шовк", 6, 1100, 1100)]

        public void CalculateFinalPrice_WhenNotUrgent_ShouldApplyOnlyMaterialMarkup(
            string material, int days, decimal basePrice, decimal expectedPrice)
        {
            // Arrange
            var order = new ClothingOrder(material, "Полтава", days, basePrice, "M"); //"M" та "Полтава" як заглушки

            // Act
            decimal actualPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }
        [Theory]                                //тестуємо ціну для одягу, враховуючи матеріал ,терміново(<5днів)         
        [InlineData("Бавовна", 4, 1000, 1200)] // Звичайний матеріал, терміново : 1000 * 1.2 = 1200
        [InlineData("Льон", 4, 1100, 1320)]    // Преміум + терміново: 1100 * 1.1 * 1.2 = 1320
        [InlineData("Шовк", 4, 1100, 1320)]    
        public void CalculateFinalPrice_WhenUrgent_ShouldApplyUrgentMultiplier(
            string material, int days, decimal basePrice, decimal expectedPrice)
        {
            // Arrange
            var order = new ClothingOrder(material, "Полтава", days, basePrice, "M");

            // Act
            decimal actualPrice = order.CalculateFinalPrice();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }
        [Theory]                                                    //Тести логіки Pattern Matching (Technique & Category)
        [InlineData("Полтава", "Вишивка біллю (білим по білому)")]
        [InlineData("Гуцульщина", "Низинка або кучерявий шов")]
        [InlineData("Сучасний", "Машинна вишивка або принт")]
        [InlineData("_", "Техніка обирається майстром")] // Дефолтний випадок (_)
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
    }
}