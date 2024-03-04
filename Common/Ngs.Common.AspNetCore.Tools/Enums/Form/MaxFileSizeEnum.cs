namespace Ngs.Common.AspNetCore.Tools.Enums.Form;

/// <summary>
/// Max file size enum
/// </summary>
public enum MaxFileSizeEnum : long
{
    /// <summary>
    /// 128 bytes
    /// </summary>
    _128B = 1L << 7,
    
    /// <summary>
    /// 256 bytes
    /// </summary>
    _256B = 1L << 8,
    
    /// <summary>
    /// 512 bytes
    /// </summary>
    _512B = 1L << 9,
    
    /// <summary>
    /// 1 kilobyte
    /// </summary>
    _1KB = 1L << 10,
    
    /// <summary>
    /// 2 kilobytes
    /// </summary>
    _2KB = 1L << 11,
    
    /// <summary>
    /// 3 kilobytes
    /// </summary>
    _3KB = 1L << 11 | 1L << 10,
    
    /// <summary>
    /// 4 kilobytes
    /// </summary>
    _4KB = 1L << 12,
    
    /// <summary>
    /// 5 kilobytes
    /// </summary>
    _5KB = 1L << 12 | 1L << 10,
    
    /// <summary>
    /// 6 kilobytes
    /// </summary>
    _6KB = 1L << 12 | 1L << 11,
    
    /// <summary>
    /// 7 kilobytes
    /// </summary>
    _7KB = 1L << 12 | 1L << 11 | 1L << 10,
    
    /// <summary>
    /// 8 kilobytes
    /// </summary>
    _8KB = 1L << 13,
    
    /// <summary>
    /// 9 kilobytes
    /// </summary>
    _9KB = 1L << 13 | 1L << 10,
    
    /// <summary>
    /// 10 kilobytes
    /// </summary>
    _10KB = 1L << 13 | 1L << 11,
    
    /// <summary>
    /// 12 kilobytes
    /// </summary>
    _12KB = 1L << 13 | 1L << 12,
    
    /// <summary>
    /// 16 kilobytes
    /// </summary>
    _16KB = 1L << 14,
    
    /// <summary>
    /// 24 kilobytes
    /// </summary>
    _24KB = 1L << 14 | 1L << 13,
    
    /// <summary>
    /// 32 kilobytes
    /// </summary>
    _32KB = 1L << 15,
    
    /// <summary>
    /// 48 kilobytes
    /// </summary>
    _48KB = 1L << 15 | 1L << 14,
    
    /// <summary>
    /// 64 kilobytes
    /// </summary>
    _64KB = 1L << 16,
    
    /// <summary>
    /// 96 kilobytes
    /// </summary>
    _96KB = 1L << 16 | 1L << 15,
    
    /// <summary>
    /// 128 kilobytes
    /// </summary>
    _128KB = 1L << 17,
    
    /// <summary>
    /// 192 kilobytes
    /// </summary>
    _192KB = 1L << 17 | 1L << 16,
    
    /// <summary>
    /// 256 kilobytes
    /// </summary>
    _256KB = 1L << 18,
    
    /// <summary>
    /// 384 kilobytes
    /// </summary>
    _384KB = 1L << 18 | 1L << 17,
    
    /// <summary>
    /// 512 kilobytes
    /// </summary>
    _512KB = 1L << 19,
    
    /// <summary>
    /// 768 kilobytes
    /// </summary>
    _768KB = 1L << 19 | 1L << 18,
    
    /// <summary>
    /// 1 megabyte
    /// </summary>
    _1MB = 1L << 20,
    
    /// <summary>
    /// 1.5 megabytes
    /// </summary>
    _2MB = 1L << 21,
    
    /// <summary>
    /// 3 megabytes
    /// </summary>
    _3MB = 1L << 21 | 1L << 20,
    
    /// <summary>
    /// 4 megabytes
    /// </summary>
    _4MB = 1L << 22,
    
    /// <summary>
    /// 5 megabytes
    /// </summary>
    _5MB = 1L << 22 | 1L << 20,
    
    /// <summary>
    /// 6 megabytes
    /// </summary>
    _6MB = 1L << 22 | 1L << 21,
    
    /// <summary>
    /// 7 megabytes
    /// </summary>
    _7MB = 1L << 22 | 1L << 21 | 1L << 20,
    
    /// <summary>
    /// 8 megabytes
    /// </summary>
    _8MB = 1L << 23,
    
    /// <summary>
    /// 9 megabytes
    /// </summary>
    _9MB = 1L << 23 | 1L << 20,
    
    /// <summary>
    /// 10 megabytes
    /// </summary>
    _10MB = 1L << 23 | 1L << 21,
    
    /// <summary>
    /// 12 megabytes
    /// </summary>
    _12MB = 1L << 23 | 1L << 22,
    
    /// <summary>
    /// 16 megabytes
    /// </summary>
    _16MB = 1L << 24,
    
    /// <summary>
    /// 24 megabytes
    /// </summary>
    _24MB = 1L << 24 | 1L << 23,
    
    /// <summary>
    /// 32 megabytes
    /// </summary>
    _32MB = 1L << 25,
    
    /// <summary>
    /// 48 megabytes
    /// </summary>
    _48MB = 1L << 25 | 1L << 24,
    
    /// <summary>
    /// 64 megabytes
    /// </summary>
    _64MB = 1L << 26,
    
    /// <summary>
    /// 96 megabytes
    /// </summary>
    _96MB = 1L << 26 | 1L << 25,
    
    /// <summary>
    /// 128 megabytes
    /// </summary>
    _128MB = 1L << 27,
    
    /// <summary>
    /// 192 megabytes
    /// </summary>
    _192MB = 1L << 27 | 1L << 26,
    
    /// <summary>
    /// 256 megabytes
    /// </summary>
    _256MB = 1L << 28,
    
    /// <summary>
    /// 384 megabytes
    /// </summary>
    _384MB = 1L << 28 | 1L << 27,
    
    /// <summary>
    /// 512 megabytes
    /// </summary>
    _512MB = 1L << 29,
    
    /// <summary>
    /// 768 megabytes
    /// </summary>
    _768MB = 1L << 29 | 1L << 28,
    
    /// <summary>
    /// 1 gigabyte
    /// </summary>
    _1GB = 1L << 30,
    
    /// <summary>
    /// 1.5 gigabytes
    /// </summary>
    _1_5GB = 1L << 30 | 1L << 29,
    
    /// <summary>
    /// 2 gigabytes
    /// </summary>
    _2GB = 1L << 31,
    
    /// <summary>
    /// 2.5 gigabytes
    /// </summary>
    _2_5GB = 1L << 31 | 1L << 30,
    
    /// <summary>
    /// 3 gigabytes
    /// </summary>
    _3GB = 1L << 31 | 1L << 30 | 1L << 29,
    
    /// <summary>
    /// 3.5 gigabytes
    /// </summary>
    _3_5GB = 1L << 31 | 1L << 30 | 1L << 29 | 1L << 28,
    
    /// <summary>
    /// 4 gigabytes
    /// </summary>
    _4GB = 1L << 32,
    
    /// <summary>
    /// 5 gigabytes
    /// </summary>
    _5GB = 1L << 32 | 1L << 30,
    
    /// <summary>
    /// 8 gigabytes
    /// </summary>
    _8GB = 1L << 33,
    
    /// <summary>
    /// 10 gigabytes
    /// </summary>
    _10GB = 1L << 33 | 1L << 30,
    
    /// <summary>
    /// 12 gigabytes
    /// </summary>
    _12GB = 1L << 33 | 1L << 32,
    
    /// <summary>
    /// 16 gigabytes
    /// </summary>
    _16GB = 1L << 34,
    
    /// <summary>
    /// 20 gigabytes
    /// </summary>
    _20GB = 1L << 34 | 1L << 32,
    
    /// <summary>
    /// 24 gigabytes
    /// </summary>
    _24GB = 1L << 34 | 1L << 33,
    
    /// <summary>
    /// 32 gigabytes
    /// </summary>
    _32GB = 1L << 35,
    
    /// <summary>
    /// 48 gigabytes
    /// </summary>
    _48GB = 1L << 35 | 1L << 34,
    
    /// <summary>
    /// 64 gigabytes
    /// </summary>
    _64GB = 1L << 36,
    
    /// <summary>
    /// 96 gigabytes
    /// </summary>
    _96GB = 1L << 36 | 1L << 35,
    
    /// <summary>
    /// 128 gigabytes
    /// </summary>
    _128GB = 1L << 37,
    
    /// <summary>
    /// 192 gigabytes
    /// </summary>
    _192GB = 1L << 37 | 1L << 36,
    
    /// <summary>
    /// 256 gigabytes
    /// </summary>
    _256GB = 1L << 38,
    
    /// <summary>
    /// 512 gigabytes
    /// </summary>
    _512GB = 1L << 39,
    
    /// <summary>
    /// 768 gigabytes
    /// </summary>
    _768GB = 1L << 39 | 1L << 38,
    
    /// <summary>
    /// 1 terabyte
    /// </summary>
    _1TB = 1L << 40,
    
    /// <summary>
    /// 2 terabytes
    /// </summary>
    _2TB = 1L << 41,
    
    /// <summary>
    /// 3 terabytes
    /// </summary>
    _4TB = 1L << 42,
    
    /// <summary>
    /// 4 terabytes
    /// </summary>
    _8TB = 1L << 43
}