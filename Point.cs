using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLabor10;


namespace labor121
{
    public class Point<T> where T : IInit, new()
    {
        public T Data { get; set; }
        public Point<T>? Next { get; set; }
        public Point<T>? Pred { get; set; }
        public static Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }
        public static T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }
        public Point()
        {
            this.Data = default(T);
            this.Next = null;
            this.Pred = null;
        }
        public Point(T data)
        {
            this.Data = data;
            this.Next = null;
            this.Pred = null;
        }
        public override string? ToString()
        {
            return Data == null ? "" : Data.ToString();
        }



    }
}
