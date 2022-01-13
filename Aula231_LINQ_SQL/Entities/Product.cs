/* >>> CLASSE PRODUCT <<< */
using System.Globalization;

namespace Aula231_LINQ_SQL.Entities
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; } // Associacao

        public override string ToString()
        {
            return Id + ", " + Name + ", " + Price.ToString("F2", CultureInfo.InvariantCulture) + ", "
                + Category.Name + ", " + Category.Tier;
        }
    }
}
