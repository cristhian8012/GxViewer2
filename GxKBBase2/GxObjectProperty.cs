using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxKBBase18
{
    public class GxObjectProperty
    {
        public int EntityTypeId { get; set; }
        public int PropertyId { get; set; }

        /// <summary>
        /// Nombre lógico de la propiedad (AutoRefresh, Theme, etc.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valor largo (XML / texto completo)
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Valor corto (cuando GeneXus lo usa)
        /// </summary>
        public string ShortValue { get; set; }

        /// <summary>
        /// Valor ya normalizado para uso humano
        /// </summary>
        public string HumanValue
        {
            get
            {
                if (!string.IsNullOrEmpty(Value))
                    return Value;

                return ShortValue;
            }
        }
    }
}
