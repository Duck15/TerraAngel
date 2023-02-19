﻿using System.Reflection;

namespace TerraAngel.Utility;

public class TileUtil
{
    // guys i finally moved them to arrays
    public static int[] TileToItem = new int[] { 2, 3, 62, 4041, 8, -1, 11, 12, 13, 14, 25, -1, 5286, 31, 32, 34, 35, 33, 36, 94, 27, 48, 56, 59, -1, 61, -1, 63, -1, 87, 9, -1, -1, 105, 106, 1813, 1869, 116, 129, 131, 133, 134, 136, 137, 139, 141, 143, 145, 147, 148, 149, 150, -1, 169, 170, 171, 173, 172, 174, 176, 195, -1, -1, 4642, 4644, 4643, 4641, 4640, 4645, -1, 194, -1, -1, -1, -1, 192, 214, 221, 222, 224, -1, 275, 307, -1, -1, 321, 332, 333, 334, 335, 336, 337, 341, 342, 352, 344, 345, 346, 347, -1, 349, 354, 355, 356, 359, 360, 363, 364, 365, 369, -1, 366, 370, -1, 398, -1, 408, 409, 412, 413, 414, 415, 416, 424, 480, 487, 488, -1, -1, 502, 511, 512, 513, 524, 525, 529, 538, 539, 540, 562, 577, 580, 581, 582, 583, 586, 591, 593, 594, 596, 604, 607, 609, 611, 612, 613, 614, 619, 620, 621, 662, 664, 4391, 833, 834, -1, 699, 700, 701, 702, 1872, 1873, 2827, 714, 713, 717, 718, 719, 181, 4349, 4350, 4351, 4352, 4353, -1, -1, -1, -1, 276, 751, 183, 832, 933, 762, 766, 763, 765, 767, 775, 2171, 835, -1, 824, 836, 880, -1, 883, 909, 911, 928, 937, 947, 951, 965, 85, 966, 970, 995, 996, 997, 998, 1104, 1105, 1106, 1103, 1129, 1101, 1107, 1120, 1125, 1127, -1, 1150, -1, 1246, 1263, -1, 1292, -1, 20, 1360, 1417, 1421, 1430, 1449, 1474, 1479, 1551, 1589, 1591, 1593, 1725, 1727, 1729, 1828, -1, -1, -1, -1, -1, -1, -1, 1970, 1971, 1972, 1973, 1974, 1975, 1976, -1, 1993, 2005, 1344, 2119, 2120, 2162, 2163, 2164, 2165, 2166, 2167, 2168, 250, 2172, 2173, 2174, 2175, 2177, 2178, 2179, 2180, 2181, 2182, 2183, 2184, 2185, 2186, 2187, 2190, 2191, 2192, 2193, 2194, 2195, 2196, 2197, 2198, 2203, 2204, 2206, 2207, 2260, 2261, 2262, 2340, 2435, 2439, 2440, 2441, 2490, 2496, 2503, 2504, -1, 2625, 2692, 2693, 2694, 2695, 2697, 71, 72, 73, 74, -1, 2700, 2701, 2702, 2738, 2741, 2751, 2752, 2753, 2754, 2755, 2787, 2792, 2793, 2794, 470, 2860, 2868, -1, 2996, 2999, 3000, 3064, 3066, 3070, 3071, 3072, 3073, 3074, 3075, 3076, 3077, 3078, 3081, 3086, 3087, 3100, 3113, 3117, 3182, 3184, 3185, 2334, 3198, 3202, 3214, 3215, 4354, -1, 3360, 3361, 3234, -1, 3239, 3240, -1, 3253, 3254, 3255, 3256, 3257, 3270, 3271, 3272, 3274, 3275, 3276, 3277, 3338, 3339, 3347, 3364, 3365, 3380, 3460, 3461, 3536, 3545, 3549, 3565, 3566, 3573, 3574, 3575, 3576, 3602, 3603, 3609, 3610, 3613, 3616, 3617, 3621, 3622, 3630, 3629, 3633, 3634, 3635, 3636, 3637, 3638, 3639, 3640, 3641, 3642, 3644, 3665, 3707, 3722, 5066, 3725, 3736, 3737, 3738, 3739, 3740, 3741, 3742, 3744, 3746, 3747, 3748, 3749, 3754, 3755, 3756, 3782, 3795, 3813, 3814, 3815, 3816, 3884, 3886, 3920, 498, 2699, 3951, 3953, 3955, 3977, 4040, -1, 4050, 4051, 4054, 4238, 4239, 4240, 4390, 5067, 4063, 4064, -1, 4074, 4075, 4076, -1, 4083, 4089, 4090, 4091, 4096, 4139, 4142, 4229, 4230, 4231, 4232, -1, 4275, 4276, 4277, 4278, 4318, 4319, 4320, -1, -1, -1, -1, -1, -1, -1, -1, 4326, 4327, 4328, 4329, 4330, 4331, 4332, 4333, -1, -1, -1, 4355, 4364, 4376, 4377, -1, 4378, -1, 4380, 4389, -1, 4392, 4396, 4398, 4399, 4420, 4422, 4430, 4435, -1, 4461, 4462, 4460, 4473, 4474, 4475, 4476, -1, 4481, 4483, 4599, 4554, 4564, 4547, 4553, 4552, 4646, 4609, 4655, 4656, 4657, -1, 4695, 4710, 4717, 4718, 4719, 4720, 4721, 4761, 4846, 4848, 4850, -1, -1, -1, -1, -1, -1, -1, 4851, 4858, 4867, 4868, 4869, 4871, -1, 4876, 4880, 4882, 4883, 4884, 4885, 4886, 4887, 4888, 4889, 4890, 4891, 4892, 4893, 4894, 4895, 4904, 4906, 4907, -1, 4924, 4962, 4963, 4964, 3750, 5008, 5081, -1, 5127, -1, 5128, -1, 5133, 5137, 5138, 5213, 5214, -1, 5215, -1, -1, -1, 5287, 5301, 5306, 5296, 5314, 5315, 5316, 5322, 5324, -1, -1, -1, -1, -1, -1, 5327, -1, -1, 5345, 5347, 5349, 5351, -1, -1, 5381, 5383, 5384, 5395, 5398, -1, 5401, 5402, 5403, 5404, 5405, 5406, 5407, 5408, 5417, 5419, 5421, 5423, 5425, 5427, 5433, 5435, 5429, 5431, 5439, 5440, 5441, 5442, 5443, 5444, };
    public static int[] WallToItem = new int[] { -1, 26, -1, -1, 93, 130, 132, 5365, 5371, 5368, 142, 144, 146, -1, -1, -1, 30, 135, 138, 140, 330, 392, 417, 418, 419, 420, 421, 479, -1, 587, 592, 595, 605, 606, 608, 610, 615, 616, 617, 618, -1, 622, 623, 624, 663, 720, 721, 722, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 3584, -1, 5363, -1, -1, -1, 745, 746, 747, -1, -1, -1, 750, 752, 764, 768, 769, 770, 1723, -1, -1, -1, 825, -1, 884, 927, -1, 5376, 1267, 1268, 1269, 1270, 1271, 1272, 5366, 5367, 5369, 5370, 5372, 5373, 1378, 1379, 1380, 1381, 1382, 1383, 1447, 1448, 1126, 1590, 1592, 1594, 1102, 1726, 1728, 1730, 1948, 1949, 1950, 1951, 1952, 1953, 1954, 1955, 1956, 1957, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2158, 2159, 2160, 2169, 2170, 2210, 2211, 2212, 2213, 2263, 2264, 2271, 2333, 2432, 2433, 2434, 2505, 2507, 2506, 2508, 2677, 2679, 2681, 2683, 2678, 2680, 2682, 2684, 2686, 2688, 2690, 2685, 2687, 2689, 2691, 2696, 2698, -1, -1, 2788, 2789, 2790, 2791, 2861, 3067, -1, 3083, -1, 3089, 3101, 3082, 3088, -1, 3238, 5374, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 5375, -1, -1, -1, -1, -1, -1, -1, 3472, 3751, 3752, 3753, 3760, 3761, 3762, 3952, 3954, 3956, 4052, 4053, 4140, 4233, 4234, 4235, 4236, 4260, 4279, 4280, -1, 4424, 4486, 4487, 4488, 4489, 4490, 4491, 4492, 4493, 4494, 4495, 4496, 4497, 4498, 4499, 4500, 4501, 4502, 4503, 4504, 4505, 4506, 4507, 4508, 4509, 4510, 4511, -1, -1, 4512, 3273, 4513, 4514, 4515, 4516, 4517, 4518, 4519, 4520, 4521, 4522, 4523, 4524, 4525, 4526, 4527, 4528, 4529, 4530, 4531, 4532, 4533, 4534, 4535, 4536, 4537, 4538, 4539, 4540, 3340, 3341, 3342, 3343, 3344, 3345, 3346, 3348, 4565, 4548, 4647, 4667, 5216, 5217, 5291, 5307, 5396, 5397, 5399, 5409, 5410, 5411, 5412, 5413, 5414, 5415, 5416, 5418, 5420, 5422, 5424, 5426, 5428, 5434, 5436, 5430, 5432, 5445, 5446, 5447, 5448, 5449, 5450, };

    public static Color[] PaintColors = new Color[32];

    public static Color[] colorLookup = new Color[0];
    public static ushort[] wallLookup = new ushort[0];
    public static ushort[] tileLookup = new ushort[0];

    
    public static Color[] TileColor = new Color[0];
    public static Color[] WallColor = new Color[0];

    public static Color GetWallColor(int type)
    {
        return WallColor[type];
    }
    public static Color GetWallColor(int type, int paint)
    {
        Color wallColor = WallColor[type];
        switch (paint)
        {
            case 29:
                {
                    Color color = PaintColors[paint];
                    float num = (float)(int)wallColor.R / 255f;
                    float num2 = (float)(int)wallColor.G / 255f;
                    float num3 = (float)(int)wallColor.B / 255f;
                    if (num2 > num)
                    {
                        float num4 = num;
                        num = num2;
                    }
                    if (num3 > num)
                    {
                        float num5 = num;
                        num = num3;
                    }
                    float num7 = num3 * 0.3f;
                    wallColor.R = (byte)((float)(int)color.R * num7);
                    wallColor.G = (byte)((float)(int)color.G * num7);
                    wallColor.B = (byte)((float)(int)color.B * num7);
                    break;
                }
            case 30:
                {
                    wallColor.R = (byte)((255 - wallColor.R) / 2);
                    wallColor.G = (byte)((255 - wallColor.G) / 2);
                    wallColor.B = (byte)((255 - wallColor.B) / 2);
                }
                break;
            case 31:
            case 0:
                break;
            default:
                {
                    Color color = PaintColors[paint];

                    float num = (float)(int)wallColor.R / 255f;
                    float num2 = (float)(int)wallColor.G / 255f;
                    float num3 = (float)(int)wallColor.B / 255f;
                    if (num2 > num)
                    {
                        float num4 = num;
                        num = num2;
                    }
                    if (num3 > num)
                    {
                        float num5 = num;
                        num = num3;
                    }
                    float num6 = num;
                    wallColor.R = (byte)((float)color.R * num6);
                    wallColor.G = (byte)((float)color.G * num6);
                    wallColor.B = (byte)((float)color.B * num6);
                    break;
                }
        }
        return wallColor;
    }

    public static Color GetTileColor(int type)
    {
        return TileColor[type];
    }
    public static Color GetTileColor(int type, int paint)
    {
        Color tileColor = TileColor[type];

        switch (paint)
        {
            case 29:
                {
                    Color color = PaintColors[paint];
                    float num = (float)(int)tileColor.R / 255f;
                    float num2 = (float)(int)tileColor.G / 255f;
                    float num3 = (float)(int)tileColor.B / 255f;
                    if (num2 > num)
                    {
                        float num4 = num;
                        num = num2;
                    }
                    if (num3 > num)
                    {
                        float num5 = num;
                        num = num3;
                    }
                    float num7 = num3 * 0.3f;
                    tileColor.R = (byte)((float)(int)color.R * num7);
                    tileColor.G = (byte)((float)(int)color.G * num7);
                    tileColor.B = (byte)((float)(int)color.B * num7);
                    break;
                }
            case 30:
                {
                    tileColor.R = (byte)(255 - tileColor.R);
                    tileColor.G = (byte)(255 - tileColor.G);
                    tileColor.B = (byte)(255 - tileColor.B);
                }
                break;
            case 0:
                break;
            default:
                {
                    Color color = PaintColors[paint];

                    float num = (float)(int)tileColor.R / 255f;
                    float num2 = (float)(int)tileColor.G / 255f;
                    float num3 = (float)(int)tileColor.B / 255f;
                    if (num2 > num)
                    {
                        float num4 = num;
                        num = num2;
                    }
                    if (num3 > num)
                    {
                        float num5 = num;
                        num = num3;
                    }
                    float num6 = num;
                    tileColor.R = (byte)((float)color.R * num6);
                    tileColor.G = (byte)((float)color.G * num6);
                    tileColor.B = (byte)((float)color.B * num6);
                    break;
                }
        }
        return tileColor;
    }

    private static void LoadPrivateArrays()
    {
        colorLookup = (Color[])((Color[])typeof(Terraria.Map.MapHelper).GetField("colorLookup", BindingFlags.Static | BindingFlags.NonPublic)!.GetValue(null)!).Clone();
        wallLookup = (ushort[])((ushort[])typeof(Terraria.Map.MapHelper).GetField("wallLookup", BindingFlags.Static | BindingFlags.Public)!.GetValue(null)!).Clone();
        tileLookup = (ushort[])((ushort[])typeof(Terraria.Map.MapHelper).GetField("tileLookup", BindingFlags.Static | BindingFlags.Public)!.GetValue(null)!).Clone();
    }
    private static void LoadDefaultData()
    {
        TileColor = new Color[tileLookup.Length];
        WallColor = new Color[wallLookup.Length];

        for (int i = 0; i < TileColor.Length; i++)
        {
            TileColor[i] = colorLookup[tileLookup[i]];
        }

        for (int i = 0; i < WallColor.Length; i++)
        {
            WallColor[i] = colorLookup[wallLookup[i]];
        }

        for (int i = 0; i < PaintColors.Length; i++)
        {
            PaintColors[i] = WorldGen.paintColor(i);
        }
    }

    public static void Init()
    {
        LoadPrivateArrays();
        LoadDefaultData();
    }
}