

What will serve the most fast for 64 bit mask? What if we need to reverse the mask and store in byte array?

https://github.com/invertedtomato/integer-compression/issues/10

```
|                                Method |       N |      Mean |     Error |    StdDev | Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 |  Allocated |
|-------------------------------------- |-------- |----------:|----------:|----------:|------:|--------:|-----------:|------:|------:|-----------:|
|       thread_usnafe_shared_bool_array | 1000000 |  32.48 ms | 0.6017 ms | 0.5629 ms |  1.00 |    0.00 |          - |     - |     - |          - |
|            bool_create_each_timearray | 1000000 |  50.33 ms | 1.0042 ms | 2.5008 ms |  1.56 |    0.09 | 27000.0000 |     - |     - | 88000000 B |
| bool_create_one_local_for_N_timearray | 1000000 |  42.06 ms | 0.8140 ms | 1.2184 ms |  1.29 |    0.04 |          - |     - |     - |       88 B |
|       bool_array_pre_thead_with_check | 1000000 |  32.67 ms | 0.6529 ms | 1.0165 ms |  1.01 |    0.04 |          - |     - |     - |          - |
|                      bool_stack_array | 1000000 |  33.71 ms | 0.6720 ms | 1.0262 ms |  1.04 |    0.05 |          - |     - |     - |          - |
|                write_from_bits_number | 1000000 |  16.88 ms | 0.3346 ms | 0.7483 ms |  0.53 |    0.02 |          - |     - |     - |          - |
|                 write_from_bits_array | 1000000 |  80.60 ms | 1.3949 ms | 1.3048 ms |  2.48 |    0.06 |          - |     - |     - |          - |
|                    u64BitsWriteBitRef | 1000000 | 117.02 ms | 1.7502 ms | 1.5515 ms |  3.61 |    0.09 |          - |     - |     - |          - |
|                           u64OneByOne | 1000000 |  40.38 ms | 0.6963 ms | 1.0840 ms |  1.26 |    0.05 |          - |     - |     - |          - |
|                    u64BitsWriteBitSet | 1000000 | 122.92 ms | 1.7655 ms | 1.6514 ms |  3.79 |    0.09 |          - |     - |     - |          - |

```