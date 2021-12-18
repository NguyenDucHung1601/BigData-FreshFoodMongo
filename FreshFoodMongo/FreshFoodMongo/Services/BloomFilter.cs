using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FreshFoodMongo
{

    /// <summary>
    /// Bloom filter.
    /// </summary>
    /// <typeparam name="T">Kiểu đối tượng đưa qua bộ lọc</typeparam>
    public class Filter<T>
    {
        public readonly int _hashFunctionCount;
        public readonly BitArray _hashBits;
        public readonly HashFunction _getHashSecondary;

        /// <summary>
        /// khởi tạo 1 bloom filter , với tỉ lệ dương sai (false positive = 1/capacity) 
        /// </summary>
        /// <param name="capacity">Số lượng bản ghi dự kiến ​​sẽ được thêm vào bộ lọc. Có thể thêm nhiều hơn, nhưng tỷ lệ lỗi sẽ vượt quá những gì mong đợi.</param>
        public Filter(int capacity)
            : this(capacity, null)
        {
        }

        /// <summary>
        /// Tạo bộ lọc Bloom mới, sử dụng kích thước tối ưu cho cấu trúc dữ liệu bên dưới dựa trên dung lượng và tỷ lệ lỗi mong muốn, cũng như số lượng hàm băm tối ưu.
        /// </summary>
        /// <param name="capacity">Số lượng bản ghi dự kiến ​​sẽ được thêm vào bộ lọc. Có thể thêm nhiều hơn, nhưng tỷ lệ lỗi sẽ vượt quá những gì mong đợi.</param>
        /// <param name="errorRate">Tỷ lệ dương tính giả có thể chấp nhận được</param>
        public Filter(int capacity, float errorRate)
            : this(capacity, errorRate, null)
        {
        }

        public Filter(int capacity, HashFunction hashFunction)
            : this(capacity, BestErrorRate(capacity), hashFunction)
        {
        }

        public Filter(int capacity, float errorRate, HashFunction hashFunction)
            : this(capacity, errorRate, hashFunction, BestM(capacity, errorRate), BestK(capacity, errorRate))
        {
        }

        /// <summary>
        /// Khởi tạo bộ lọc bloom filter
        /// </summary>
        /// <param name="capacity">Số lượng bản ghi dự kiến ​​sẽ được thêm vào bộ lọc. Có thể thêm nhiều hơn, nhưng tỷ lệ lỗi sẽ vượt quá những gì mong đợi.</param>
        /// <param name="errorRate">Tỷ lệ dương tính giả có thể chấp nhận được</param>
        /// <param name="hashFunction">Hàm băm để băm giá trị đầu vào.</param>
        /// <param name="m">Số lượng phần tử của mảng bit. </param>
        /// <param name="k">Số lượng hàm băm sử dụng.</param>
        public Filter(int capacity, float errorRate, HashFunction hashFunction, int m, int k)
        {
            // validate the params are in range
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "capacity must be > 0");
            }

            if (errorRate >= 1 || errorRate <= 0)
            {
                throw new ArgumentOutOfRangeException("errorRate", errorRate, string.Format("errorRate must be between 0 and 1, exclusive. Was {0}", errorRate));
            }

            // from overflow in bestM calculation
            if (m < 1)
            {
                throw new ArgumentOutOfRangeException(string.Format("The provided capacity and errorRate values would result in an array of length > int.MaxValue. Please reduce either of these values. Capacity: {0}, Error rate: {1}", capacity, errorRate));
            }

            // set the secondary hash function
            if (hashFunction == null)
            {
                if (typeof(T) == typeof(string))
                {
                    this._getHashSecondary = HashString;
                }
                else if (typeof(T) == typeof(int))
                {
                    this._getHashSecondary = HashInt32;
                }
                else
                {
                    throw new ArgumentNullException("hashFunction", "Please provide a hash function for your type T, when T is not a string or int.");
                }
            }
            else
            {
                this._getHashSecondary = hashFunction;
            }

            this._hashFunctionCount = k;
            this._hashBits = new BitArray(m);
        }

        /// <summary>
        /// Hàm bắm có thể sử dụng để bắm giá trị đầu vào
        /// </summary>
        /// <param name="input">giá trị đầu vào hàm băm</param>
        /// <returns> trả về kết quả hàm băm </returns>
        public delegate int HashFunction(T input);

       

        /// <summary>
        /// Thêm một phần tử vào bộ lọc - không thể loại bỏ
        /// </summary>
        /// <param name="item"> Phần tử đưa vào bộ lọc.</param>
        public void Add(T item)
        {
            // Bật bit tương ứng trên mảng bit cho mỗi hàm băm của phần tử
            int primaryHash = item.GetHashCode();
            int secondaryHash = this._getHashSecondary(item);
            for (int i = 0; i < this._hashFunctionCount; i++)
            {
                int hash = this.ComputeHash(primaryHash, secondaryHash, i);
                this._hashBits[hash] = true;
            }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của phần tử trong bộ lọc, cho xác suất nhất định.
        /// </summary>
        /// <param name="item"> phần tử cần kiểm tra </param>
        /// <returns> trả về kết quả true or false </returns>
        public bool Contains(T item)
        {
            int primaryHash = item.GetHashCode();
            int secondaryHash = this._getHashSecondary(item);
            for (int i = 0; i < this._hashFunctionCount; i++)
            {
                int hash = this.ComputeHash(primaryHash, secondaryHash, i);
                if (this._hashBits[hash] == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// tính số hàm băm tối ưu
        /// </summary>
        /// <param name="capacity">  capacity (n): số lượng phần tử dự kiến đi qua bộ lọc </param>
        /// <param name="errorRate"> tỷ lệ dương tính giả. </param>
        /// <returns> The <see cref="int"/>. </returns>
        private static int BestK(int capacity, float errorRate)
        {
            return (int)Math.Round(Math.Log(2.0) * BestM(capacity, errorRate) / capacity);
        }

        /// <summary>
        /// tính m tối ưu.(số lượng phần tử của mảng bit)
        /// </summary>
        /// <param name="capacity">  capacity (n): số lượng phần tử dự kiến đi qua bộ lọc </param>
        /// <param name="errorRate"> tỷ lệ dương tính giả. </param>
        /// <returns> trả về số lượng phần tử tối ưu của mảng bit </returns>
        private static int BestM(int capacity, float errorRate)
        {
            return (int)Math.Ceiling((capacity * Math.Log(errorRate)) / Math.Log(1 / Math.Pow(2, Math.Log(2))));
        }

        /// <summary>
        /// Tỷ lệ dương tính giả (dương sai)
        /// </summary>
        /// <param name="capacity">  capacity (n): số lượng phần tử dự kiến đi qua bộ lọc </param>
        /// <returns> trả về tỷ lệ dương tính giả tối ưu </returns>
        public static float BestErrorRate(int capacity)
        {
            float c = (float)(1.0 / capacity);
            if (c != 0)
            {
                return c;
            }

            // default
            // http://www.cs.princeton.edu/courses/archive/spring02/cs493/lec7.pdf
            return (float)Math.Pow(0.6185, int.MaxValue / capacity);
        }

        /// <summary>
        /// Băm một số nguyên (int) 32 bit bằng cách sử dụng phương pháp của Thomas Wang v3.1(http://www.concentric.net/~Ttwang/tech/inthash.htm).
        /// Thời gian chạy được đề xuất là 11 chu kỳ.
        /// </summary>
        /// <param name="input"> Giá trị đưa vào để băm.</param>
        /// <returns> trả về kết quả sai khi băm.</returns>
        private static int HashInt32(T input)
        {
            uint? x = input as uint?;
            unchecked
            {
                x = ~x + (x << 15); // x = (x << 15) - x- 1, as (~x) + y is equivalent to y - x - 1 in two's complement representation
                x = x ^ (x >> 12);
                x = x + (x << 2);
                x = x ^ (x >> 4);
                x = x * 2057; // x = (x + (x << 3)) + (x<< 11);
                x = x ^ (x >> 16);
                return (int)x;
            }
        }


        /// <summary>
        /// Băm một chuỗi bằng phương pháp "One At A Time" của Bob Jenkin từ Tiến sĩ Dobbs (http://burtleburtle.net/bob/hash/doobs.html).
        /// Thời gian chạy được đề xuất là 9x + 9, trong đó x = độ dài chuỗi cần băm(input.Length).
        /// </summary>
        /// <param name="input">Chuỗi đầu vào để băm.</param>
        /// <returns> Kết quả sau khi băm.</returns>
        private static int HashString(T input)
        {
            string s = input as string;
            int hash = 0;

            for (int i = 0; i < s.Length; i++)
            {
                hash += s[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        

        /// <summary>
        /// Thực hiện băm kép
        /// </summary>
        /// <param name="primaryHash"> Hàm băm chính thứ 1 </param>
        /// <param name="secondaryHash"> Hàm băm thứ 2. </param>
        /// <param name="i"> The i. </param>
        /// <returns> The <see cref="int"/>. </returns>
        private int ComputeHash(int primaryHash, int secondaryHash, int i)
        {
            int resultingHash = (primaryHash + (i * secondaryHash)) % this._hashBits.Count; 
            return Math.Abs((int)resultingHash);
        }
    }
}
