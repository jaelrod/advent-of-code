using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode
{
    public class Day13
    {
        public static int Main13(string[] args)
        {
            string input = Day13.Input;
            ClawMachine[] clawMachines;
            string errorMessage;

            if (!Day13.TryParse(input, out clawMachines, out errorMessage))//, 10000000000000))
            {
                Console.WriteLine(errorMessage);
                Console.ReadKey();
                return 1;
            }

            // long minCost = clawMachines.Sum(cm => cm.MinCost());
            long minCost = 0;
            foreach (var cm in clawMachines)
            {
                Console.WriteLine(cm);
                long cost = cm.MinCost();
                Console.WriteLine($"cost: {cost}");
                minCost += cost;
            }
            Console.WriteLine($"minCost: {minCost}");
            return 0;
        }

        private static bool TryParse(string input, out ClawMachine[] clawMachines, out string errorMessage, long prizeError = 0)
        {
            clawMachines = null;
            errorMessage = null;

            string[] pieces = input.Split($"{Environment.NewLine}{Environment.NewLine}");
            ClawMachine[] clawMachinesTemp = new ClawMachine[pieces.Length];
            for (int i = 0; i < pieces.Length; ++i)
            {
                if (!ClawMachine.TryParse(pieces[i], out clawMachinesTemp[i], out errorMessage, prizeError))
                {
                    return false;
                }
            }

            clawMachines = clawMachinesTemp;
            return true;
        }

        private const string Example = "Button A: X+94, Y+34\r\nButton B: X+22, Y+67\r\nPrize: X=8400, Y=5400\r\n\r\nButton A: X+26, Y+66\r\nButton B: X+67, Y+21\r\nPrize: X=12748, Y=12176\r\n\r\nButton A: X+17, Y+86\r\nButton B: X+84, Y+37\r\nPrize: X=7870, Y=6450\r\n\r\nButton A: X+69, Y+23\r\nButton B: X+27, Y+71\r\nPrize: X=18641, Y=10279";
        private const string Debug = "Button A: X+46, Y+32\r\nButton B: X+23, Y+84\r\nPrize: X=3473, Y=4524";
        private const string Input = "Button A: X+53, Y+35\r\nButton B: X+11, Y+23\r\nPrize: X=13386, Y=18840\r\n\r\nButton A: X+57, Y+35\r\nButton B: X+19, Y+39\r\nPrize: X=12698, Y=18002\r\n\r\nButton A: X+62, Y+29\r\nButton B: X+45, Y+65\r\nPrize: X=4448, Y=4366\r\n\r\nButton A: X+90, Y+19\r\nButton B: X+48, Y+75\r\nPrize: X=6270, Y=3594\r\n\r\nButton A: X+98, Y+19\r\nButton B: X+35, Y+45\r\nPrize: X=7133, Y=3714\r\n\r\nButton A: X+26, Y+22\r\nButton B: X+13, Y+96\r\nPrize: X=2288, Y=9416\r\n\r\nButton A: X+24, Y+19\r\nButton B: X+29, Y+80\r\nPrize: X=4054, Y=6746\r\n\r\nButton A: X+85, Y+55\r\nButton B: X+39, Y+85\r\nPrize: X=7812, Y=8820\r\n\r\nButton A: X+50, Y+21\r\nButton B: X+14, Y+28\r\nPrize: X=16748, Y=18645\r\n\r\nButton A: X+19, Y+48\r\nButton B: X+56, Y+35\r\nPrize: X=7401, Y=19267\r\n\r\nButton A: X+12, Y+87\r\nButton B: X+81, Y+66\r\nPrize: X=4092, Y=6732\r\n\r\nButton A: X+37, Y+14\r\nButton B: X+14, Y+52\r\nPrize: X=9288, Y=10480\r\n\r\nButton A: X+86, Y+45\r\nButton B: X+48, Y+86\r\nPrize: X=4280, Y=4614\r\n\r\nButton A: X+38, Y+89\r\nButton B: X+93, Y+52\r\nPrize: X=10769, Y=9138\r\n\r\nButton A: X+86, Y+11\r\nButton B: X+64, Y+73\r\nPrize: X=7738, Y=4036\r\n\r\nButton A: X+46, Y+32\r\nButton B: X+23, Y+84\r\nPrize: X=3473, Y=4524\r\n\r\nButton A: X+13, Y+95\r\nButton B: X+79, Y+74\r\nPrize: X=4673, Y=4957\r\n\r\nButton A: X+11, Y+29\r\nButton B: X+51, Y+36\r\nPrize: X=5976, Y=7050\r\n\r\nButton A: X+24, Y+50\r\nButton B: X+57, Y+16\r\nPrize: X=7772, Y=10678\r\n\r\nButton A: X+20, Y+31\r\nButton B: X+46, Y+20\r\nPrize: X=5710, Y=7236\r\n\r\nButton A: X+21, Y+83\r\nButton B: X+73, Y+28\r\nPrize: X=1962, Y=6973\r\n\r\nButton A: X+59, Y+39\r\nButton B: X+22, Y+72\r\nPrize: X=2874, Y=6324\r\n\r\nButton A: X+88, Y+47\r\nButton B: X+12, Y+24\r\nPrize: X=1848, Y=1761\r\n\r\nButton A: X+14, Y+34\r\nButton B: X+66, Y+24\r\nPrize: X=3004, Y=10704\r\n\r\nButton A: X+59, Y+14\r\nButton B: X+35, Y+81\r\nPrize: X=3587, Y=7321\r\n\r\nButton A: X+21, Y+70\r\nButton B: X+97, Y+85\r\nPrize: X=6916, Y=6370\r\n\r\nButton A: X+20, Y+36\r\nButton B: X+42, Y+16\r\nPrize: X=7756, Y=6896\r\n\r\nButton A: X+95, Y+26\r\nButton B: X+25, Y+93\r\nPrize: X=9255, Y=3653\r\n\r\nButton A: X+86, Y+48\r\nButton B: X+12, Y+64\r\nPrize: X=5052, Y=4768\r\n\r\nButton A: X+32, Y+72\r\nButton B: X+98, Y+64\r\nPrize: X=8008, Y=7376\r\n\r\nButton A: X+58, Y+93\r\nButton B: X+40, Y+12\r\nPrize: X=2904, Y=2988\r\n\r\nButton A: X+26, Y+58\r\nButton B: X+50, Y+18\r\nPrize: X=14824, Y=744\r\n\r\nButton A: X+21, Y+88\r\nButton B: X+89, Y+89\r\nPrize: X=6676, Y=7815\r\n\r\nButton A: X+62, Y+43\r\nButton B: X+12, Y+28\r\nPrize: X=3770, Y=11095\r\n\r\nButton A: X+92, Y+52\r\nButton B: X+13, Y+73\r\nPrize: X=4940, Y=3580\r\n\r\nButton A: X+24, Y+49\r\nButton B: X+53, Y+25\r\nPrize: X=3554, Y=9129\r\n\r\nButton A: X+12, Y+33\r\nButton B: X+43, Y+30\r\nPrize: X=19546, Y=3989\r\n\r\nButton A: X+30, Y+64\r\nButton B: X+43, Y+26\r\nPrize: X=3611, Y=4614\r\n\r\nButton A: X+36, Y+42\r\nButton B: X+16, Y+97\r\nPrize: X=4444, Y=12313\r\n\r\nButton A: X+66, Y+21\r\nButton B: X+13, Y+44\r\nPrize: X=3591, Y=775\r\n\r\nButton A: X+71, Y+17\r\nButton B: X+79, Y+76\r\nPrize: X=5591, Y=4250\r\n\r\nButton A: X+15, Y+37\r\nButton B: X+72, Y+25\r\nPrize: X=1872, Y=2939\r\n\r\nButton A: X+21, Y+50\r\nButton B: X+50, Y+31\r\nPrize: X=8024, Y=6548\r\n\r\nButton A: X+72, Y+99\r\nButton B: X+83, Y+13\r\nPrize: X=11442, Y=7845\r\n\r\nButton A: X+32, Y+12\r\nButton B: X+63, Y+85\r\nPrize: X=14461, Y=3519\r\n\r\nButton A: X+33, Y+59\r\nButton B: X+23, Y+13\r\nPrize: X=13887, Y=5765\r\n\r\nButton A: X+50, Y+21\r\nButton B: X+27, Y+51\r\nPrize: X=2429, Y=17498\r\n\r\nButton A: X+22, Y+75\r\nButton B: X+61, Y+12\r\nPrize: X=13447, Y=13595\r\n\r\nButton A: X+67, Y+37\r\nButton B: X+15, Y+29\r\nPrize: X=16951, Y=1877\r\n\r\nButton A: X+82, Y+95\r\nButton B: X+97, Y+20\r\nPrize: X=5415, Y=3225\r\n\r\nButton A: X+13, Y+30\r\nButton B: X+93, Y+50\r\nPrize: X=9652, Y=6800\r\n\r\nButton A: X+20, Y+28\r\nButton B: X+39, Y+16\r\nPrize: X=16877, Y=17536\r\n\r\nButton A: X+46, Y+16\r\nButton B: X+13, Y+23\r\nPrize: X=13392, Y=2282\r\n\r\nButton A: X+69, Y+37\r\nButton B: X+20, Y+48\r\nPrize: X=9614, Y=11250\r\n\r\nButton A: X+13, Y+49\r\nButton B: X+59, Y+14\r\nPrize: X=19015, Y=3427\r\n\r\nButton A: X+13, Y+37\r\nButton B: X+41, Y+14\r\nPrize: X=12739, Y=2356\r\n\r\nButton A: X+54, Y+16\r\nButton B: X+20, Y+59\r\nPrize: X=7018, Y=9826\r\n\r\nButton A: X+59, Y+11\r\nButton B: X+19, Y+78\r\nPrize: X=9180, Y=11088\r\n\r\nButton A: X+45, Y+18\r\nButton B: X+43, Y+61\r\nPrize: X=4435, Y=2212\r\n\r\nButton A: X+87, Y+50\r\nButton B: X+32, Y+84\r\nPrize: X=5376, Y=8404\r\n\r\nButton A: X+43, Y+71\r\nButton B: X+25, Y+11\r\nPrize: X=18994, Y=2502\r\n\r\nButton A: X+28, Y+55\r\nButton B: X+41, Y+18\r\nPrize: X=4778, Y=6424\r\n\r\nButton A: X+64, Y+25\r\nButton B: X+28, Y+48\r\nPrize: X=1936, Y=2980\r\n\r\nButton A: X+49, Y+19\r\nButton B: X+23, Y+45\r\nPrize: X=1313, Y=18963\r\n\r\nButton A: X+28, Y+62\r\nButton B: X+55, Y+19\r\nPrize: X=9538, Y=11506\r\n\r\nButton A: X+30, Y+12\r\nButton B: X+13, Y+47\r\nPrize: X=3851, Y=4907\r\n\r\nButton A: X+41, Y+72\r\nButton B: X+70, Y+38\r\nPrize: X=5243, Y=5980\r\n\r\nButton A: X+50, Y+17\r\nButton B: X+31, Y+63\r\nPrize: X=8695, Y=10046\r\n\r\nButton A: X+16, Y+39\r\nButton B: X+19, Y+11\r\nPrize: X=8118, Y=11867\r\n\r\nButton A: X+12, Y+68\r\nButton B: X+72, Y+19\r\nPrize: X=6980, Y=12648\r\n\r\nButton A: X+30, Y+86\r\nButton B: X+73, Y+34\r\nPrize: X=7539, Y=5312\r\n\r\nButton A: X+12, Y+38\r\nButton B: X+97, Y+23\r\nPrize: X=1830, Y=680\r\n\r\nButton A: X+57, Y+31\r\nButton B: X+28, Y+85\r\nPrize: X=3762, Y=6023\r\n\r\nButton A: X+90, Y+62\r\nButton B: X+23, Y+79\r\nPrize: X=3643, Y=5099\r\n\r\nButton A: X+31, Y+72\r\nButton B: X+44, Y+15\r\nPrize: X=6324, Y=16028\r\n\r\nButton A: X+11, Y+60\r\nButton B: X+89, Y+29\r\nPrize: X=889, Y=741\r\n\r\nButton A: X+48, Y+28\r\nButton B: X+13, Y+91\r\nPrize: X=1664, Y=3640\r\n\r\nButton A: X+64, Y+21\r\nButton B: X+14, Y+58\r\nPrize: X=4076, Y=15941\r\n\r\nButton A: X+38, Y+17\r\nButton B: X+11, Y+49\r\nPrize: X=14289, Y=7126\r\n\r\nButton A: X+66, Y+33\r\nButton B: X+19, Y+52\r\nPrize: X=2616, Y=7533\r\n\r\nButton A: X+63, Y+82\r\nButton B: X+91, Y+25\r\nPrize: X=10563, Y=7301\r\n\r\nButton A: X+60, Y+30\r\nButton B: X+34, Y+66\r\nPrize: X=11898, Y=8912\r\n\r\nButton A: X+65, Y+33\r\nButton B: X+11, Y+78\r\nPrize: X=7122, Y=10278\r\n\r\nButton A: X+68, Y+32\r\nButton B: X+11, Y+29\r\nPrize: X=4552, Y=4048\r\n\r\nButton A: X+50, Y+36\r\nButton B: X+28, Y+75\r\nPrize: X=2896, Y=2469\r\n\r\nButton A: X+36, Y+66\r\nButton B: X+36, Y+12\r\nPrize: X=3824, Y=16262\r\n\r\nButton A: X+35, Y+24\r\nButton B: X+12, Y+65\r\nPrize: X=1203, Y=1052\r\n\r\nButton A: X+77, Y+26\r\nButton B: X+66, Y+79\r\nPrize: X=8998, Y=7065\r\n\r\nButton A: X+80, Y+21\r\nButton B: X+53, Y+61\r\nPrize: X=9035, Y=6845\r\n\r\nButton A: X+36, Y+17\r\nButton B: X+23, Y+51\r\nPrize: X=16060, Y=14915\r\n\r\nButton A: X+80, Y+18\r\nButton B: X+11, Y+50\r\nPrize: X=4760, Y=11860\r\n\r\nButton A: X+39, Y+94\r\nButton B: X+65, Y+26\r\nPrize: X=4940, Y=6288\r\n\r\nButton A: X+58, Y+16\r\nButton B: X+39, Y+80\r\nPrize: X=16872, Y=7232\r\n\r\nButton A: X+46, Y+19\r\nButton B: X+50, Y+80\r\nPrize: X=3782, Y=2393\r\n\r\nButton A: X+38, Y+76\r\nButton B: X+51, Y+19\r\nPrize: X=1857, Y=1805\r\n\r\nButton A: X+41, Y+20\r\nButton B: X+29, Y+94\r\nPrize: X=5904, Y=9428\r\n\r\nButton A: X+20, Y+33\r\nButton B: X+41, Y+18\r\nPrize: X=19032, Y=2225\r\n\r\nButton A: X+61, Y+27\r\nButton B: X+19, Y+48\r\nPrize: X=9809, Y=2663\r\n\r\nButton A: X+43, Y+70\r\nButton B: X+35, Y+16\r\nPrize: X=2813, Y=5334\r\n\r\nButton A: X+84, Y+19\r\nButton B: X+11, Y+76\r\nPrize: X=880, Y=13230\r\n\r\nButton A: X+51, Y+20\r\nButton B: X+14, Y+39\r\nPrize: X=9175, Y=14274\r\n\r\nButton A: X+33, Y+20\r\nButton B: X+20, Y+39\r\nPrize: X=9845, Y=11893\r\n\r\nButton A: X+42, Y+60\r\nButton B: X+75, Y+23\r\nPrize: X=4449, Y=2401\r\n\r\nButton A: X+12, Y+63\r\nButton B: X+82, Y+32\r\nPrize: X=3222, Y=7249\r\n\r\nButton A: X+71, Y+39\r\nButton B: X+21, Y+56\r\nPrize: X=7633, Y=18263\r\n\r\nButton A: X+12, Y+54\r\nButton B: X+83, Y+29\r\nPrize: X=10556, Y=14036\r\n\r\nButton A: X+32, Y+19\r\nButton B: X+22, Y+45\r\nPrize: X=3046, Y=2096\r\n\r\nButton A: X+41, Y+14\r\nButton B: X+21, Y+31\r\nPrize: X=9594, Y=18484\r\n\r\nButton A: X+45, Y+69\r\nButton B: X+88, Y+42\r\nPrize: X=8387, Y=7377\r\n\r\nButton A: X+48, Y+19\r\nButton B: X+34, Y+59\r\nPrize: X=6862, Y=13588\r\n\r\nButton A: X+25, Y+80\r\nButton B: X+76, Y+65\r\nPrize: X=3896, Y=4270\r\n\r\nButton A: X+26, Y+94\r\nButton B: X+86, Y+20\r\nPrize: X=7332, Y=3816\r\n\r\nButton A: X+21, Y+49\r\nButton B: X+98, Y+71\r\nPrize: X=3332, Y=2887\r\n\r\nButton A: X+28, Y+56\r\nButton B: X+52, Y+28\r\nPrize: X=3780, Y=4932\r\n\r\nButton A: X+57, Y+43\r\nButton B: X+15, Y+54\r\nPrize: X=2520, Y=2584\r\n\r\nButton A: X+86, Y+36\r\nButton B: X+61, Y+93\r\nPrize: X=6006, Y=2784\r\n\r\nButton A: X+70, Y+13\r\nButton B: X+18, Y+77\r\nPrize: X=6296, Y=13842\r\n\r\nButton A: X+84, Y+27\r\nButton B: X+56, Y+69\r\nPrize: X=5264, Y=2661\r\n\r\nButton A: X+11, Y+25\r\nButton B: X+50, Y+11\r\nPrize: X=19810, Y=5751\r\n\r\nButton A: X+19, Y+50\r\nButton B: X+21, Y+11\r\nPrize: X=14441, Y=18206\r\n\r\nButton A: X+27, Y+62\r\nButton B: X+65, Y+22\r\nPrize: X=12928, Y=8592\r\n\r\nButton A: X+68, Y+22\r\nButton B: X+67, Y+82\r\nPrize: X=10281, Y=6644\r\n\r\nButton A: X+40, Y+65\r\nButton B: X+34, Y+18\r\nPrize: X=4708, Y=6086\r\n\r\nButton A: X+16, Y+53\r\nButton B: X+58, Y+13\r\nPrize: X=8520, Y=11920\r\n\r\nButton A: X+99, Y+27\r\nButton B: X+72, Y+79\r\nPrize: X=13707, Y=8428\r\n\r\nButton A: X+41, Y+14\r\nButton B: X+21, Y+44\r\nPrize: X=14527, Y=13878\r\n\r\nButton A: X+31, Y+11\r\nButton B: X+27, Y+45\r\nPrize: X=13928, Y=6032\r\n\r\nButton A: X+25, Y+54\r\nButton B: X+60, Y+23\r\nPrize: X=900, Y=7491\r\n\r\nButton A: X+43, Y+78\r\nButton B: X+52, Y+20\r\nPrize: X=17700, Y=3516\r\n\r\nButton A: X+15, Y+32\r\nButton B: X+44, Y+15\r\nPrize: X=9890, Y=7278\r\n\r\nButton A: X+16, Y+35\r\nButton B: X+74, Y+50\r\nPrize: X=15554, Y=13400\r\n\r\nButton A: X+38, Y+71\r\nButton B: X+94, Y+16\r\nPrize: X=4276, Y=5116\r\n\r\nButton A: X+24, Y+48\r\nButton B: X+57, Y+33\r\nPrize: X=16631, Y=5759\r\n\r\nButton A: X+14, Y+34\r\nButton B: X+62, Y+19\r\nPrize: X=17908, Y=5151\r\n\r\nButton A: X+51, Y+13\r\nButton B: X+26, Y+56\r\nPrize: X=9069, Y=8243\r\n\r\nButton A: X+49, Y+81\r\nButton B: X+97, Y+15\r\nPrize: X=10767, Y=8787\r\n\r\nButton A: X+21, Y+75\r\nButton B: X+74, Y+21\r\nPrize: X=10175, Y=11444\r\n\r\nButton A: X+55, Y+94\r\nButton B: X+69, Y+19\r\nPrize: X=5011, Y=6190\r\n\r\nButton A: X+34, Y+15\r\nButton B: X+11, Y+29\r\nPrize: X=17791, Y=19222\r\n\r\nButton A: X+58, Y+23\r\nButton B: X+35, Y+69\r\nPrize: X=7256, Y=2377\r\n\r\nButton A: X+37, Y+84\r\nButton B: X+88, Y+51\r\nPrize: X=5035, Y=7860\r\n\r\nButton A: X+12, Y+75\r\nButton B: X+82, Y+11\r\nPrize: X=9268, Y=17544\r\n\r\nButton A: X+14, Y+53\r\nButton B: X+40, Y+21\r\nPrize: X=2490, Y=9657\r\n\r\nButton A: X+88, Y+36\r\nButton B: X+12, Y+42\r\nPrize: X=3992, Y=2820\r\n\r\nButton A: X+27, Y+11\r\nButton B: X+12, Y+24\r\nPrize: X=275, Y=2867\r\n\r\nButton A: X+30, Y+53\r\nButton B: X+95, Y+54\r\nPrize: X=6580, Y=6616\r\n\r\nButton A: X+17, Y+42\r\nButton B: X+67, Y+27\r\nPrize: X=13750, Y=16175\r\n\r\nButton A: X+24, Y+75\r\nButton B: X+39, Y+12\r\nPrize: X=1793, Y=13514\r\n\r\nButton A: X+50, Y+26\r\nButton B: X+18, Y+34\r\nPrize: X=16886, Y=15478\r\n\r\nButton A: X+24, Y+84\r\nButton B: X+47, Y+13\r\nPrize: X=3348, Y=6264\r\n\r\nButton A: X+18, Y+97\r\nButton B: X+83, Y+17\r\nPrize: X=7870, Y=2825\r\n\r\nButton A: X+65, Y+22\r\nButton B: X+11, Y+31\r\nPrize: X=4764, Y=2949\r\n\r\nButton A: X+80, Y+14\r\nButton B: X+77, Y+93\r\nPrize: X=2266, Y=1828\r\n\r\nButton A: X+14, Y+52\r\nButton B: X+68, Y+11\r\nPrize: X=19444, Y=7189\r\n\r\nButton A: X+31, Y+14\r\nButton B: X+21, Y+50\r\nPrize: X=1023, Y=10942\r\n\r\nButton A: X+26, Y+68\r\nButton B: X+47, Y+15\r\nPrize: X=19560, Y=9422\r\n\r\nButton A: X+50, Y+19\r\nButton B: X+24, Y+59\r\nPrize: X=4686, Y=9547\r\n\r\nButton A: X+47, Y+30\r\nButton B: X+18, Y+40\r\nPrize: X=18351, Y=9410\r\n\r\nButton A: X+13, Y+82\r\nButton B: X+84, Y+12\r\nPrize: X=2131, Y=17074\r\n\r\nButton A: X+40, Y+63\r\nButton B: X+44, Y+18\r\nPrize: X=6724, Y=16721\r\n\r\nButton A: X+14, Y+34\r\nButton B: X+72, Y+34\r\nPrize: X=5358, Y=19794\r\n\r\nButton A: X+13, Y+93\r\nButton B: X+71, Y+79\r\nPrize: X=2984, Y=5048\r\n\r\nButton A: X+54, Y+21\r\nButton B: X+45, Y+87\r\nPrize: X=3771, Y=4872\r\n\r\nButton A: X+52, Y+16\r\nButton B: X+51, Y+64\r\nPrize: X=8179, Y=6816\r\n\r\nButton A: X+43, Y+21\r\nButton B: X+35, Y+52\r\nPrize: X=1065, Y=8212\r\n\r\nButton A: X+98, Y+30\r\nButton B: X+29, Y+95\r\nPrize: X=6018, Y=10110\r\n\r\nButton A: X+77, Y+29\r\nButton B: X+19, Y+61\r\nPrize: X=18548, Y=764\r\n\r\nButton A: X+17, Y+42\r\nButton B: X+56, Y+28\r\nPrize: X=12014, Y=872\r\n\r\nButton A: X+62, Y+25\r\nButton B: X+12, Y+48\r\nPrize: X=860, Y=1210\r\n\r\nButton A: X+42, Y+12\r\nButton B: X+26, Y+60\r\nPrize: X=4792, Y=3872\r\n\r\nButton A: X+19, Y+43\r\nButton B: X+66, Y+40\r\nPrize: X=17818, Y=7888\r\n\r\nButton A: X+15, Y+83\r\nButton B: X+72, Y+13\r\nPrize: X=11900, Y=6871\r\n\r\nButton A: X+84, Y+15\r\nButton B: X+32, Y+94\r\nPrize: X=10428, Y=7689\r\n\r\nButton A: X+44, Y+69\r\nButton B: X+41, Y+13\r\nPrize: X=15438, Y=17599\r\n\r\nButton A: X+61, Y+12\r\nButton B: X+14, Y+40\r\nPrize: X=9312, Y=6208\r\n\r\nButton A: X+57, Y+13\r\nButton B: X+89, Y+96\r\nPrize: X=926, Y=514\r\n\r\nButton A: X+13, Y+31\r\nButton B: X+31, Y+19\r\nPrize: X=13328, Y=10754\r\n\r\nButton A: X+63, Y+27\r\nButton B: X+29, Y+92\r\nPrize: X=6381, Y=9180\r\n\r\nButton A: X+80, Y+32\r\nButton B: X+27, Y+51\r\nPrize: X=1603, Y=1003\r\n\r\nButton A: X+15, Y+68\r\nButton B: X+74, Y+26\r\nPrize: X=15281, Y=15880\r\n\r\nButton A: X+23, Y+53\r\nButton B: X+40, Y+13\r\nPrize: X=17000, Y=8297\r\n\r\nButton A: X+56, Y+79\r\nButton B: X+75, Y+25\r\nPrize: X=7992, Y=6103\r\n\r\nButton A: X+25, Y+38\r\nButton B: X+61, Y+12\r\nPrize: X=5445, Y=2626\r\n\r\nButton A: X+58, Y+12\r\nButton B: X+20, Y+57\r\nPrize: X=1558, Y=2754\r\n\r\nButton A: X+55, Y+36\r\nButton B: X+18, Y+77\r\nPrize: X=940, Y=3224\r\n\r\nButton A: X+38, Y+77\r\nButton B: X+89, Y+59\r\nPrize: X=2675, Y=4571\r\n\r\nButton A: X+26, Y+64\r\nButton B: X+72, Y+48\r\nPrize: X=5834, Y=8416\r\n\r\nButton A: X+76, Y+24\r\nButton B: X+22, Y+46\r\nPrize: X=6986, Y=4354\r\n\r\nButton A: X+11, Y+24\r\nButton B: X+91, Y+60\r\nPrize: X=8475, Y=6576\r\n\r\nButton A: X+30, Y+56\r\nButton B: X+53, Y+15\r\nPrize: X=3225, Y=3057\r\n\r\nButton A: X+43, Y+73\r\nButton B: X+45, Y+13\r\nPrize: X=2180, Y=17082\r\n\r\nButton A: X+12, Y+60\r\nButton B: X+87, Y+16\r\nPrize: X=7638, Y=5508\r\n\r\nButton A: X+81, Y+99\r\nButton B: X+77, Y+20\r\nPrize: X=8966, Y=5993\r\n\r\nButton A: X+71, Y+19\r\nButton B: X+32, Y+79\r\nPrize: X=1297, Y=2108\r\n\r\nButton A: X+23, Y+54\r\nButton B: X+45, Y+16\r\nPrize: X=14323, Y=15488\r\n\r\nButton A: X+29, Y+89\r\nButton B: X+91, Y+73\r\nPrize: X=2781, Y=4203\r\n\r\nButton A: X+25, Y+85\r\nButton B: X+56, Y+46\r\nPrize: X=3221, Y=8641\r\n\r\nButton A: X+54, Y+14\r\nButton B: X+16, Y+63\r\nPrize: X=14198, Y=1138\r\n\r\nButton A: X+19, Y+48\r\nButton B: X+59, Y+27\r\nPrize: X=646, Y=5318\r\n\r\nButton A: X+52, Y+32\r\nButton B: X+15, Y+33\r\nPrize: X=12307, Y=309\r\n\r\nButton A: X+92, Y+27\r\nButton B: X+36, Y+75\r\nPrize: X=5940, Y=6447\r\n\r\nButton A: X+11, Y+54\r\nButton B: X+58, Y+41\r\nPrize: X=2873, Y=4111\r\n\r\nButton A: X+11, Y+91\r\nButton B: X+74, Y+16\r\nPrize: X=4164, Y=9408\r\n\r\nButton A: X+12, Y+56\r\nButton B: X+69, Y+15\r\nPrize: X=15590, Y=16990\r\n\r\nButton A: X+60, Y+25\r\nButton B: X+17, Y+40\r\nPrize: X=4359, Y=9505\r\n\r\nButton A: X+45, Y+15\r\nButton B: X+21, Y+74\r\nPrize: X=3384, Y=5751\r\n\r\nButton A: X+71, Y+11\r\nButton B: X+67, Y+54\r\nPrize: X=11931, Y=5687\r\n\r\nButton A: X+23, Y+68\r\nButton B: X+72, Y+18\r\nPrize: X=4599, Y=288\r\n\r\nButton A: X+24, Y+55\r\nButton B: X+45, Y+15\r\nPrize: X=13142, Y=17155\r\n\r\nButton A: X+26, Y+58\r\nButton B: X+33, Y+12\r\nPrize: X=4482, Y=4198\r\n\r\nButton A: X+11, Y+53\r\nButton B: X+57, Y+17\r\nPrize: X=9011, Y=14505\r\n\r\nButton A: X+85, Y+49\r\nButton B: X+15, Y+28\r\nPrize: X=8360, Y=5187\r\n\r\nButton A: X+35, Y+13\r\nButton B: X+13, Y+36\r\nPrize: X=12734, Y=15532\r\n\r\nButton A: X+13, Y+39\r\nButton B: X+78, Y+44\r\nPrize: X=8164, Y=6632\r\n\r\nButton A: X+78, Y+51\r\nButton B: X+27, Y+75\r\nPrize: X=2172, Y=3714\r\n\r\nButton A: X+54, Y+14\r\nButton B: X+32, Y+81\r\nPrize: X=538, Y=13823\r\n\r\nButton A: X+18, Y+52\r\nButton B: X+95, Y+70\r\nPrize: X=7772, Y=9368\r\n\r\nButton A: X+13, Y+82\r\nButton B: X+76, Y+78\r\nPrize: X=2603, Y=9194\r\n\r\nButton A: X+30, Y+60\r\nButton B: X+32, Y+18\r\nPrize: X=3524, Y=12146\r\n\r\nButton A: X+11, Y+83\r\nButton B: X+87, Y+78\r\nPrize: X=4478, Y=8915\r\n\r\nButton A: X+64, Y+14\r\nButton B: X+16, Y+63\r\nPrize: X=2416, Y=3325\r\n\r\nButton A: X+47, Y+12\r\nButton B: X+50, Y+87\r\nPrize: X=5655, Y=8975\r\n\r\nButton A: X+86, Y+44\r\nButton B: X+15, Y+52\r\nPrize: X=1721, Y=1900\r\n\r\nButton A: X+66, Y+11\r\nButton B: X+81, Y+67\r\nPrize: X=3501, Y=1065\r\n\r\nButton A: X+71, Y+13\r\nButton B: X+41, Y+83\r\nPrize: X=7586, Y=6598\r\n\r\nButton A: X+15, Y+32\r\nButton B: X+27, Y+14\r\nPrize: X=17672, Y=6940\r\n\r\nButton A: X+47, Y+74\r\nButton B: X+77, Y+12\r\nPrize: X=5265, Y=7088\r\n\r\nButton A: X+40, Y+67\r\nButton B: X+40, Y+15\r\nPrize: X=10200, Y=16809\r\n\r\nButton A: X+94, Y+81\r\nButton B: X+11, Y+79\r\nPrize: X=6908, Y=10402\r\n\r\nButton A: X+16, Y+96\r\nButton B: X+74, Y+69\r\nPrize: X=3486, Y=7791\r\n\r\nButton A: X+64, Y+17\r\nButton B: X+33, Y+63\r\nPrize: X=2316, Y=1266\r\n\r\nButton A: X+36, Y+21\r\nButton B: X+17, Y+48\r\nPrize: X=2272, Y=1478\r\n\r\nButton A: X+84, Y+23\r\nButton B: X+12, Y+62\r\nPrize: X=17156, Y=3886\r\n\r\nButton A: X+15, Y+65\r\nButton B: X+62, Y+11\r\nPrize: X=2006, Y=4108\r\n\r\nButton A: X+77, Y+42\r\nButton B: X+12, Y+51\r\nPrize: X=4828, Y=13100\r\n\r\nButton A: X+11, Y+49\r\nButton B: X+52, Y+18\r\nPrize: X=5750, Y=14200\r\n\r\nButton A: X+23, Y+58\r\nButton B: X+71, Y+34\r\nPrize: X=10022, Y=15596\r\n\r\nButton A: X+24, Y+44\r\nButton B: X+40, Y+18\r\nPrize: X=3568, Y=10058\r\n\r\nButton A: X+59, Y+11\r\nButton B: X+19, Y+75\r\nPrize: X=8550, Y=7606\r\n\r\nButton A: X+52, Y+25\r\nButton B: X+41, Y+66\r\nPrize: X=18087, Y=13277\r\n\r\nButton A: X+38, Y+14\r\nButton B: X+29, Y+47\r\nPrize: X=12227, Y=7001\r\n\r\nButton A: X+95, Y+24\r\nButton B: X+53, Y+55\r\nPrize: X=8917, Y=5748\r\n\r\nButton A: X+16, Y+48\r\nButton B: X+53, Y+20\r\nPrize: X=11109, Y=5156\r\n\r\nButton A: X+25, Y+46\r\nButton B: X+36, Y+17\r\nPrize: X=9825, Y=17335\r\n\r\nButton A: X+24, Y+56\r\nButton B: X+74, Y+42\r\nPrize: X=4386, Y=4162\r\n\r\nButton A: X+25, Y+78\r\nButton B: X+45, Y+18\r\nPrize: X=5010, Y=4860\r\n\r\nButton A: X+72, Y+86\r\nButton B: X+83, Y+17\r\nPrize: X=4023, Y=1109\r\n\r\nButton A: X+38, Y+16\r\nButton B: X+32, Y+58\r\nPrize: X=9958, Y=4062\r\n\r\nButton A: X+12, Y+66\r\nButton B: X+76, Y+79\r\nPrize: X=7292, Y=7901\r\n\r\nButton A: X+25, Y+62\r\nButton B: X+45, Y+17\r\nPrize: X=7715, Y=6630\r\n\r\nButton A: X+60, Y+25\r\nButton B: X+23, Y+67\r\nPrize: X=7223, Y=13927\r\n\r\nButton A: X+79, Y+55\r\nButton B: X+13, Y+78\r\nPrize: X=4632, Y=4328\r\n\r\nButton A: X+20, Y+84\r\nButton B: X+87, Y+65\r\nPrize: X=662, Y=978\r\n\r\nButton A: X+22, Y+62\r\nButton B: X+69, Y+25\r\nPrize: X=11534, Y=17094\r\n\r\nButton A: X+64, Y+71\r\nButton B: X+42, Y+13\r\nPrize: X=6310, Y=5690\r\n\r\nButton A: X+84, Y+85\r\nButton B: X+99, Y+23\r\nPrize: X=10965, Y=6542\r\n\r\nButton A: X+32, Y+18\r\nButton B: X+14, Y+24\r\nPrize: X=16122, Y=968\r\n\r\nButton A: X+54, Y+54\r\nButton B: X+15, Y+56\r\nPrize: X=6399, Y=10458\r\n\r\nButton A: X+14, Y+43\r\nButton B: X+43, Y+28\r\nPrize: X=7512, Y=5016\r\n\r\nButton A: X+19, Y+51\r\nButton B: X+45, Y+17\r\nPrize: X=15569, Y=18213\r\n\r\nButton A: X+28, Y+15\r\nButton B: X+26, Y+53\r\nPrize: X=19000, Y=3362\r\n\r\nButton A: X+72, Y+30\r\nButton B: X+17, Y+46\r\nPrize: X=6139, Y=6300\r\n\r\nButton A: X+28, Y+39\r\nButton B: X+29, Y+13\r\nPrize: X=4125, Y=3965\r\n\r\nButton A: X+71, Y+11\r\nButton B: X+19, Y+84\r\nPrize: X=15516, Y=11946\r\n\r\nButton A: X+14, Y+58\r\nButton B: X+65, Y+17\r\nPrize: X=2804, Y=9164\r\n\r\nButton A: X+83, Y+53\r\nButton B: X+28, Y+88\r\nPrize: X=6175, Y=5065\r\n\r\nButton A: X+17, Y+83\r\nButton B: X+89, Y+49\r\nPrize: X=2043, Y=1493\r\n\r\nButton A: X+47, Y+18\r\nButton B: X+24, Y+48\r\nPrize: X=6617, Y=5438\r\n\r\nButton A: X+40, Y+16\r\nButton B: X+12, Y+49\r\nPrize: X=4232, Y=5052\r\n\r\nButton A: X+62, Y+30\r\nButton B: X+12, Y+49\r\nPrize: X=17510, Y=15422\r\n\r\nButton A: X+80, Y+72\r\nButton B: X+13, Y+85\r\nPrize: X=5774, Y=7982\r\n\r\nButton A: X+17, Y+36\r\nButton B: X+76, Y+30\r\nPrize: X=3583, Y=2088\r\n\r\nButton A: X+42, Y+16\r\nButton B: X+26, Y+49\r\nPrize: X=15122, Y=6635\r\n\r\nButton A: X+53, Y+24\r\nButton B: X+14, Y+49\r\nPrize: X=11581, Y=18716\r\n\r\nButton A: X+86, Y+46\r\nButton B: X+11, Y+52\r\nPrize: X=15118, Y=2046\r\n\r\nButton A: X+27, Y+82\r\nButton B: X+57, Y+14\r\nPrize: X=5331, Y=2666\r\n\r\nButton A: X+32, Y+79\r\nButton B: X+57, Y+13\r\nPrize: X=11138, Y=17121\r\n\r\nButton A: X+76, Y+27\r\nButton B: X+16, Y+66\r\nPrize: X=19352, Y=13364\r\n\r\nButton A: X+74, Y+21\r\nButton B: X+27, Y+49\r\nPrize: X=6446, Y=2408\r\n\r\nButton A: X+58, Y+30\r\nButton B: X+35, Y+65\r\nPrize: X=12741, Y=2295\r\n\r\nButton A: X+42, Y+18\r\nButton B: X+31, Y+65\r\nPrize: X=1149, Y=16831\r\n\r\nButton A: X+56, Y+52\r\nButton B: X+25, Y+88\r\nPrize: X=6615, Y=10224\r\n\r\nButton A: X+16, Y+55\r\nButton B: X+32, Y+21\r\nPrize: X=2672, Y=3934\r\n\r\nButton A: X+20, Y+38\r\nButton B: X+87, Y+27\r\nPrize: X=4930, Y=2452\r\n\r\nButton A: X+26, Y+41\r\nButton B: X+46, Y+22\r\nPrize: X=6082, Y=11236\r\n\r\nButton A: X+25, Y+38\r\nButton B: X+50, Y+13\r\nPrize: X=2825, Y=892\r\n\r\nButton A: X+33, Y+12\r\nButton B: X+23, Y+41\r\nPrize: X=19025, Y=17153\r\n\r\nButton A: X+48, Y+92\r\nButton B: X+76, Y+36\r\nPrize: X=5436, Y=8116\r\n\r\nButton A: X+48, Y+26\r\nButton B: X+30, Y+64\r\nPrize: X=2868, Y=2222\r\n\r\nButton A: X+22, Y+74\r\nButton B: X+70, Y+17\r\nPrize: X=9994, Y=14649\r\n\r\nButton A: X+51, Y+59\r\nButton B: X+14, Y+83\r\nPrize: X=1295, Y=5974\r\n\r\nButton A: X+64, Y+13\r\nButton B: X+23, Y+66\r\nPrize: X=13619, Y=10648\r\n\r\nButton A: X+46, Y+94\r\nButton B: X+43, Y+21\r\nPrize: X=1762, Y=1862\r\n\r\nButton A: X+42, Y+73\r\nButton B: X+35, Y+14\r\nPrize: X=1866, Y=11520\r\n\r\nButton A: X+29, Y+52\r\nButton B: X+30, Y+11\r\nPrize: X=9581, Y=2018\r\n\r\nButton A: X+69, Y+74\r\nButton B: X+91, Y+19\r\nPrize: X=1914, Y=638\r\n\r\nButton A: X+36, Y+18\r\nButton B: X+41, Y+65\r\nPrize: X=14203, Y=4129\r\n\r\nButton A: X+58, Y+19\r\nButton B: X+21, Y+68\r\nPrize: X=12321, Y=5298\r\n\r\nButton A: X+30, Y+77\r\nButton B: X+61, Y+14\r\nPrize: X=16876, Y=13962\r\n\r\nButton A: X+19, Y+57\r\nButton B: X+57, Y+18\r\nPrize: X=1906, Y=7763\r\n\r\nButton A: X+41, Y+13\r\nButton B: X+22, Y+54\r\nPrize: X=12351, Y=3275\r\n\r\nButton A: X+22, Y+64\r\nButton B: X+41, Y+12\r\nPrize: X=14891, Y=16492\r\n\r\nButton A: X+25, Y+64\r\nButton B: X+62, Y+24\r\nPrize: X=19382, Y=9816\r\n\r\nButton A: X+11, Y+32\r\nButton B: X+62, Y+33\r\nPrize: X=13818, Y=13270\r\n\r\nButton A: X+13, Y+47\r\nButton B: X+70, Y+40\r\nPrize: X=4058, Y=5822\r\n\r\nButton A: X+31, Y+57\r\nButton B: X+52, Y+31\r\nPrize: X=2796, Y=1975\r\n\r\nButton A: X+28, Y+81\r\nButton B: X+61, Y+14\r\nPrize: X=3748, Y=5668\r\n\r\nButton A: X+76, Y+22\r\nButton B: X+11, Y+94\r\nPrize: X=3577, Y=4214\r\n\r\nButton A: X+89, Y+11\r\nButton B: X+94, Y+95\r\nPrize: X=11414, Y=4746\r\n\r\nButton A: X+20, Y+46\r\nButton B: X+49, Y+28\r\nPrize: X=14532, Y=5270\r\n\r\nButton A: X+22, Y+13\r\nButton B: X+34, Y+80\r\nPrize: X=3288, Y=7155\r\n\r\nButton A: X+72, Y+27\r\nButton B: X+14, Y+59\r\nPrize: X=15070, Y=9175\r\n\r\nButton A: X+21, Y+16\r\nButton B: X+26, Y+68\r\nPrize: X=690, Y=1104\r\n\r\nButton A: X+20, Y+52\r\nButton B: X+61, Y+42\r\nPrize: X=4553, Y=5658\r\n\r\nButton A: X+78, Y+28\r\nButton B: X+17, Y+62\r\nPrize: X=5456, Y=18696\r\n\r\nButton A: X+17, Y+60\r\nButton B: X+63, Y+26\r\nPrize: X=1473, Y=11584\r\n\r\nButton A: X+79, Y+56\r\nButton B: X+16, Y+80\r\nPrize: X=2106, Y=3072\r\n\r\nButton A: X+54, Y+19\r\nButton B: X+14, Y+36\r\nPrize: X=5404, Y=2150\r\n\r\nButton A: X+55, Y+15\r\nButton B: X+28, Y+55\r\nPrize: X=7861, Y=16040\r\n\r\nButton A: X+69, Y+48\r\nButton B: X+41, Y+88\r\nPrize: X=5242, Y=3944";

        private class ClawMachine
        {
            private Move[] moves;
            private Prize prize;

            public ClawMachine(Move[] moves, Prize prize)
            {
                // sort by progress/cost to prioritize moving further
                this.moves = moves.OrderBy(m => -(double)(m.x + m.y) / (double)(m.cost)).ToArray();
                this.prize = prize;
            }

            public long MinCost()
            {
                var cache = new Dictionary<Prize, IDictionary<Move, long>>();
                long minCost = this.MinCost(this.prize, 0, cache);
                return minCost == -1 ? 0 : minCost;
            }

            // debug solution: 87 & 15
            public long MinCost(Prize remainder, int moveIndex, IDictionary<Prize, IDictionary<Move, long>> cache)
            {
                if (moveIndex >= this.moves.Length)
                {
                    return 0;
                }

                IDictionary<Move, long> minCosts;
                if (!cache.TryGetValue(remainder, out minCosts))
                {
                    minCosts = new Dictionary<Move, long>();
                    cache[remainder] = minCosts;
                }
                else if (cache[remainder].ContainsKey(this.moves[moveIndex]))
                {
                    return cache[remainder][this.moves[moveIndex]];
                }

                long remX, remY;
                long quotX = Math.DivRem(remainder.x, this.moves[moveIndex].x, out remX);
                long quotY = Math.DivRem(remainder.y, this.moves[moveIndex].y, out remY);
                if (remX == 0 && remY == 0 && quotX == quotY)
                {
                    // evenly divides remainder
                    return quotX * this.moves[moveIndex].cost;
                }

                //long maxMoves = Math.Min(remainder.x / this.moves[moveIndex].x, remainder.y / this.moves[moveIndex].y);
                //Move cell = new Move(this.moves[moveIndex].cost * maxMoves,
                //                     this.moves[moveIndex].x * maxMoves,
                //                     this.moves[moveIndex].y * maxMoves

                Move step = new Move(this.moves[moveIndex].cost, this.moves[moveIndex].x, this.moves[moveIndex].y);
                if (moveIndex < this.moves.Length-1)
                {
                    while (step.x <= remainder.x && step.y <= remainder.y)
                    {
                        var next = new Prize(remainder.x - step.x, remainder.y - step.y);
                        long minCost = this.MinCost(next, moveIndex + 1, cache);
                        if (minCost > 0)
                        {
                            return step.cost + minCost;
                        }

                        step.cost += this.moves[moveIndex].cost;
                        step.x += this.moves[moveIndex].x;
                        step.y += this.moves[moveIndex].y;
                    }
                }

                return 0;
            }

            public long MinCostNButton()
            {
                var cost = this.MinCostNButton(this.prize, 0).FirstOrDefault();
                return cost != null ? cost : 0;
            }

            private IEnumerable<long> MinCostNButton(Prize remaining, int moveIndex)
            {
                // no more valid moves
                if (moveIndex >= this.moves.Length)
                {
                    yield break;
                }

                // one move of moveIndex gets the prize
                if (remaining.x == this.moves[moveIndex].x && remaining.y == this.moves[moveIndex].y)
                {
                    if (moveIndex == this.moves.Length-1)
                    {
                        // Console.WriteLine($"solution from [{remaining.x},{remaining.y}]");
                    }
                    yield return this.moves[moveIndex].cost;
                    yield break;
                }

                // cost of one move moveIndex plus MinCost of the remainder to prize
                if (remaining.x > this.moves[moveIndex].x && remaining.y > this.moves[moveIndex].y)
                {
                    foreach (var cost in
                        MinCostNButton(new Prize(remaining.x - this.moves[moveIndex].x,
                                                 remaining.y - this.moves[moveIndex].y), moveIndex))
                    {
                        yield return this.moves[moveIndex].cost + cost;
                    }
                }

                // cost of remainder of the remainder to prize with only less valuable moves
                for (int i = moveIndex + 1; i < this.moves.Length; ++i)
                {
                    foreach (var cost in this.MinCostNButton(remaining, i))
                    {
                        // Console.WriteLine($"solution from [{remaining.x},{remaining.y}]");
                        yield return cost;
                    }
                }
            }

            public override string ToString()
            {
                return "Moves: [" + string.Join(", ", this.moves.Select(m => m.ToString())) + Environment.NewLine +
                      $"Prize: [{this.prize}]";
            }

            public static bool TryParse(string input, out ClawMachine clawMachine, out string errorMessage, long prizeError = 0)
            {
                clawMachine = null;
                errorMessage = null;

                string[] lines = input.Split(Environment.NewLine);
                if (lines.Length < 3)
                {
                    errorMessage = $"Invalid input:{Environment.NewLine}{input}";
                    return false;
                }

                var moves = new Move[lines.Length - 1];
                for (int i = 0; i < lines.Length-1; ++i)
                {
                    if (!Move.TryParse(lines[i], out moves[i], out errorMessage))
                    {
                        return false;
                    }
                }

                Prize prize;
                if (!Prize.TryParse(lines[lines.Length-1], out prize, out errorMessage))
                {
                    return false;
                }
                prize.x += prizeError;
                prize.y += prizeError;

                clawMachine = new ClawMachine(moves, prize);
                return true;
            }

            public class Move
            {
                private const string prefix = "Button ";

                public long cost;
                public long x;
                public long y;

                public Move(long cost, long x, long y)
                {
                    this.cost = cost;
                    this.x = x;
                    this.y = y;
                }

                public override int GetHashCode()
                {
                    return (int)this.cost ^ (int)(1e4 * this.x) ^ (int)(1e7 * this.y);
                }

                public override bool Equals(object? obj)
                {
                    return this.Equals(obj as Move);
                }

                public bool Equals(Move other)
                {
                    return other != null
                        && this.cost == other.cost
                        && this.x == other.x
                        && this.y == other.y;
                }

                public override string ToString()
                {
                    return $"${this.cost} [{this.x},{this.y}]";
                }

                public static bool TryParse(string input, out Move move, out string errorMessage)
                {
                    move = null;
                    errorMessage = null;

                    if (!input.StartsWith(Move.prefix))
                    {
                        errorMessage = $"Invalid move input '{input}.";
                        return false;
                    }

                    string button = input.Substring(Move.prefix.Length, input.IndexOf(":") - Move.prefix.Length);
                    if (button.Length != 1)
                    {
                        errorMessage = $"Invalid movement button '{input.Substring(Move.prefix.Length, input.IndexOf(":") - Move.prefix.Length)}'";
                        return false;
                    }
                    int cost = -1;
                    if (button[0] == 'a' || button[0] == 'A')
                    {
                        cost = 3;
                    }
                    else if (button[0] == 'b' || button[0] == 'B')
                    {
                        cost = 1;
                    }
                    else
                    {
                        errorMessage = $"Invalid movement button '{button[0]}'";
                        return false;
                    }


                    string[] split = input.Split(": ");
                    if (split.Length < 2)
                    {
                        errorMessage = $"Invalid move input '{input}.";
                        return false;
                    }

                    long x = 0;
                    long y = 0;
                    foreach (var piece in split[1].Split(", "))
                    {
                        if (piece.Length < 3)
                        {
                            errorMessage = $"Invalid control input: '{piece}.";
                            return false;
                        }

                        bool negate = false;
                        if (piece[1] == '-')
                        {
                            negate = true;
                        }
                        else if (piece[1] != '+')
                        {
                            errorMessage = $"Invalid movement displacement operator: '{piece[1]}.";
                            return false;
                        }

                        long spaces;
                        if (!long.TryParse(piece.Substring(2), out spaces))
                        {
                            errorMessage = $"Failed to parse movement spaces '{piece.Substring(2)}'.";
                            return false;
                        }

                        if (piece[0] == 'x' || piece[0] == 'X')
                        {
                            x = spaces;
                        }
                        else if (piece[0] == 'y' || piece[0] == 'Y')
                        {
                            y = spaces;
                        }
                        else
                        {
                            errorMessage = $"Invalid movement dimension '{piece[0]}'";
                            return false;
                        }
                    }

                    move = new Move(cost, x, y);
                    return true;
                }
            }

            public class Prize
            {
                private const string prefix = "Prize: ";

                public long x;
                public long y;

                public Prize(long x, long y)
                {
                    this.x = x;
                    this.y = y;
                }

                public override int GetHashCode()
                {
                    return (int)(1e4 * this.x) ^ (int)(1e7 * this.y);
                }

                public override bool Equals(object? obj)
                {
                    return this.Equals(obj as Prize);
                }

                public bool Equals(Prize other)
                {
                    return other != null
                        && this.x == other.x
                        && this.y == other.y;
                }

                public override string ToString()
                {
                    return $"[{this.x},{this.y}]";
                }

                public static bool TryParse(string input, out Prize prize, out string errorMessage)
                {
                    prize = null;
                    errorMessage = null;

                    if (input.Length < Prize.prefix.Length + 3 || !input.StartsWith(Prize.prefix))
                    {
                        errorMessage = $"Invalid prize description '{input}";
                        return false;
                    }

                    string[] pieces = input.Substring("Prize: ".Length).Split(", ");
                    if (pieces.Length < 2)
                    {
                        errorMessage = $"Failed to parse prize coordinate '{input.Substring("Prize: ".Length + 2)}'.";
                        return false;
                    }

                    long x = 0;
                    long y = 0;
                    for (int i = 0; i < pieces.Length; ++i)
                    {
                        if (pieces[i].Length < 3 || pieces[i][1] != '=')
                        {
                            errorMessage = $"Failed to parse prize coordinate '{pieces[i]}'.";
                            return false;
                        }

                        long coordinate;
                        if (!long.TryParse(pieces[i].Substring(2), out coordinate))
                        {
                            errorMessage = $"Failed to parse prize coordinate '{pieces[i]}'.";
                            return false;
                        }
                        if (pieces[i][0] == 'x' || pieces[i][0] == 'X')
                        {
                            x = coordinate;
                        }
                        else if (pieces[i][0] == 'y' || pieces[i][0] == 'Y')
                        {
                            y = coordinate;
                        }
                        else
                        {
                            errorMessage = $"Invalid prize location dimension '{pieces[i][0]}'";
                            return false;
                        }
                    }

                    prize = new Prize(x, y);
                    return true;
                }
            }
        }
    }
}
