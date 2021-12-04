using System;

namespace Contoso.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }

        public int Rating { get; set; }


        public int ProductId { get; }


        public DateTime Fecha { get; set; }



    }
}
