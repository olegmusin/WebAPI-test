using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceNewsAPI.Models
{
    public class Item
    {
        public Item()
        {
           Thumbnail = new Thumbnail();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
        public Thumbnail Thumbnail { get; set; }
    }

    [ComplexType]
    public class Thumbnail
    {   
        [Key]
        public string Url { get; set; }
        public short Height { get; set; }
        public short Width { get; set; }
 
    }
}
