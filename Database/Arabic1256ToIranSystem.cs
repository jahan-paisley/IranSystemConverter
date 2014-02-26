using System.Collections.Generic;

namespace IranSystemConvertor
{
    using System;

    public static class Arabic1256ToIranSystem
    {
        private static bool BeforeLetterType2(byte before)
        {
            switch (before)
            {
                case 0xc8: /* beh */
                case 0x81: /* peh */
                case 0xca: /* teh */
                case 0xcb: /* theh */
                case 0xcc: /* jeem */

                case 0x8d: /* cheh */

                case 0xe5: /* hah */

                case 0xce: /* khah */

                case 0xd3: /* seen */

                case 0xd4: /* sheen */

                case 0xd5: /* sad */

                case 0xd6: /* dad */

                case 0xd8: /* arabic letter tah */
                case 0xd9: /* arabic letter zah */
                case 0xda: /* ain */
                case 0xdb: /* ghain */
                case 0xdd: /* feh */
                case 0xde: /* qah */
                case 0x98: /* keheh = kaf  6A9 is probably wrong / 6A9 probable estas maløusta */
                case 0xdf: /* keheh = kaf */
                case 0x90: /* gaf */
                case 0xe1: /* lam */
                case 0xe3: /* meem */
                case 0xe4: /* noon */
                case 0xc9: /* heh */
                case 0xec: /* farsi yeh */
                //case 0xec:/* farsi yeh */
                case 0xed: /* farsi yeh - two dots underneath */
                case 0xc6: /* yeh with hamza */
                    return true;
                case 0xc1: /* Hamzeye */
                case 0xc2: /* alef with madda */
                case 0xc7: /* alef */
                case 0xc3: /* alef with hamza above */
                case 0xc5: /* alef with hamza below */
                case 0xcf: /* dal */
                case 0xd0: /* thal */
                case 0xd1: /* reh */
                case 0xd2: /* zain */
                case 0x8e: /* jeh */
                case 0xe6: /* waw */
                default:
                    return false;
            }
        }

        private static byte Convert1256Iran(byte before, byte uni, byte after)
        {
            bool isolated = false; /* izolita litero */
            bool l = false; /* litero æe fino de vorto */
            bool medial = false; /* litero en mezo de vorto */
            bool initial = false; /* litero æe komenco de vorto */
            bool beforeIsLetter = false; /* antaýa signo estas (persa) litero */
            bool afterIsLetter = false; /* posta signo estas (persa) litero */

            // hamza
            if (uni == 0xc1) return 0x8f;

            /* Assume that a Persian letter is between the codes 0x0620 and 0x06D0 */
            /* Supozu ke Persa litero havas kodon inter 0x0620 kaj 0x06D0. */
            //if (before >= 0x0620 && before <= 0x06D0) before_is_letter = true;
            if (before >= 0x80 && before <= 0xFF) beforeIsLetter = BeforeLetterType2(before);
            if (after >= 0x80 && after <= 0xFF) afterIsLetter = true;
            //if (after >= 0x0620 && after <= 0x06D0) after_is_letter = afterLetterType(after);

            if (beforeIsLetter && afterIsLetter) medial = true;
            else if (beforeIsLetter && !afterIsLetter) l = true;
            else if (!beforeIsLetter && afterIsLetter) initial = true;
            else isolated = true;

            /* I can't see any systematic relationship between Unicode and 
               Iran System, so I'll just test the letters individually. */
            /* Mi ne rimarkas sisteman rilaton inter Unikodo kaj Iransistemo,
               tial mi simple testos literojn individue. */

            /* arabic comma */
            if (uni == 0xa1) return 0x8A; //a1
            /* tatweel */
            //dc
            if (uni == 0xdc) return 0x8B;
            /* arabic question mark / demando signo */
            if (uni == 0x3f) return 0x8C; //3f

            /* alef with madda */
            if (uni == 0xc2) return 0x8D; //c2
            /* yeh with hamza */
            if (uni == 0xc6) return 0x8E; //c6  /* ??? */
            /* arabic letter with hamza */
            //c6
            if (uni == 0xc6) return 0x8f; //

            /* alef */
            if (uni == 0xc2) return 0x8d; //c2
            if (uni == 0xc7 && (isolated || initial)) return 0x90; //c7
            if (uni == 0xc7 && (medial || l)) return 0x91;

            /* alef with hamza above */
            //c4 
            if (uni == 0xc4 && (isolated || initial)) return 0x90;
            if (uni == 0xc4 && (medial || l)) return 0x91;

            /* alef with hamza below */
            //c6
            if (uni == 0xc6 && (isolated || initial)) return 0x90;
            if (uni == 0xc6 && (medial || l)) return 0x91;

            /* beh */
            //c8
            if (uni == 0xc8 && (l || isolated)) return 0x92;
            if (uni == 0xc8 && (initial || medial)) return 0x93;
            /* peh */
            //81
            if (uni == 0x81 && (l || isolated)) return 0x94;
            if (uni == 0x81 && (initial || medial)) return 0x95;
            /* teh */
            //ca
            // if (uni == 0x062A && (l || isolated)) return 0x5C;
            // if (uni == 0x062A && (initial || medial)) return 0x9F;
            // Edited by Abbas because Teh is not the same as it is in IranSystem Table which can be found on wikipedia
            if (uni == 0xCA && (l || isolated)) return 0x96;
            if (uni == 0xCA && (initial || medial)) return 0x97;
            /* theh */
            //cb
            if (uni == 0xcb && (l || isolated)) return 0x98;
            if (uni == 0xcb && (initial || medial)) return 0x99;
            /* jeem */
            //cc
            if (uni == 0xcc && (l || isolated)) return 0x9A;
            if (uni == 0xcc && (initial || medial)) return 0x9B;
            /* cheh */
            //8d
            if (uni == 0x8d && (l || isolated)) return 0x9C;
            if (uni == 0x8d && (initial || medial)) return 0x9D;
            /* hah */
            //e5
            if (uni == 0xcd && (l || isolated)) return 0x9E;
            if (uni == 0xcd && (initial || medial)) return 0x9F;
            /* khah */
            //ce
            if (uni == 0xce && (l || isolated)) return 0xA0;
            if (uni == 0xce && (initial || medial)) return 0xA1;

            /* dal */
            //cf
            if (uni == 0xcf) return 0xA2;
            /* thal */
            //d0
            if (uni == 0xd0) return 0xA3;
            /* reh */
            //d1
            if (uni == 0xd1) return 0xA4;
            /* zain */
            //d2
            if (uni == 0xd2) return 0xA5;
            /* jeh */
            //8e
            if (uni == 0x8e) return 0xA6;

            /* seen */
            //d3
            if (uni == 0xd3 && (l || isolated)) return 0xA7;
            if (uni == 0xd3 && (initial || medial)) return 0xA8;
            /* sheen */
            //d4
            if (uni == 0xd4 && (l || isolated)) return 0xA9;
            if (uni == 0xd4 && (initial || medial)) return 0xAA;
            /* sad */
            //d5
            if (uni == 0xd5 && (l || isolated)) return 0xAB;
            if (uni == 0xd5 && (initial || medial)) return 0xAC;
            /* zad */
            //d6
            if (uni == 0xd6 && (l || isolated)) return 0xAD;
            if (uni == 0xd6 && (initial || medial)) return 0xAE;

            /* arabic letter tah */
            //d8
            if (uni == 0xd8) return 0xAF;

            /* arabic letter zah */
            //d9
            if (uni == 0xd9) return 0xE0;

            /* ain */
            //da
            if (uni == 0xda && isolated) return 0xE1;
            if (uni == 0xda && l) return 0xE2;
            if (uni == 0xda && medial) return 0xE3;
            if (uni == 0xda && initial) return 0xE4;
            /* ghain */
            //db
            if (uni == 0xdb && isolated) return 0xE5;
            if (uni == 0xdb && l) return 0xE6;
            if (uni == 0xdb && medial) return 0xE7;
            if (uni == 0xdb && initial) return 0xE8;

            /* feh */
            //dd
            if (uni == 0xdd && (l || isolated)) return 0xE9;
            if (uni == 0xdd && (initial || medial)) return 0xEA;
            /* qah */
            //de
            if (uni == 0xde && (l || isolated)) return 0xEB;
            if (uni == 0xde && (initial || medial)) return 0xEC;

            /* keheh = kaf  6A9 is probably wrong / 6A9 probable estas maløusta */
            //98
            if (uni == 0x98 && (l || isolated)) return 0xED;
            if (uni == 0x98 && (initial || medial)) return 0xEE;
            /* keheh = kaf */
            //df
            if (uni == 0xdf && (l || isolated)) return 0xED;
            if (uni == 0xdf && (initial || medial)) return 0xEE;

            /* gaf */
            //90
            if (uni == 0x90 && (l || isolated)) return 0xEF;
            if (uni == 0x90 && (initial || medial)) return 0xF0;
            /* lam */
            //e1
            if (uni == 0xe1 && (l || isolated)) return 0xF1;
            if (uni == 0xe1 && after == 0x0627) return 0xF2; /* ligature with alef */
            if (uni == 0xe1 && (initial || medial)) return 0xF3;

            /* meem */
            //e3
            if (uni == 0xe3 && (l || isolated)) return 0xF4;
            if (uni == 0xe3 && (initial || medial)) return 0xF5;
            /* noon */
            //e4
            if (uni == 0xe4 && (l || isolated)) return 0xF6;
            if (uni == 0xe4 && (initial || medial)) return 0xF7;

            /* waw */
            //e6
            if (uni == 0xe6) return 0xF8;

            /* heh */
            //e5
            if (uni == 0xe5 && (l || isolated)) return 0xF9;
            if (uni == 0xe5 && medial) return 0xFA;
            if (uni == 0xe5 && initial) return 0xFB;
            //c9
            if (uni == 0xc9 && (l || isolated)) return 0xF9;
            if (uni == 0xc9 && medial) return 0xFA;
            if (uni == 0xc9 && initial) return 0xFB;

            /* farsi yeh */
            //ec
            /*  Kial estas du kodoj por sama litero ??? */
            /*  Why are there two codes for the same leter? */
            if (uni == 0xec && l) return 0xFC;
            if (uni == 0xec && isolated) return 0xFD;
            if (uni == 0xec && (initial || medial)) return 0xFE;

            /* farsi yeh */
            //ec
            if (uni == 0xec && l) return 0xFC;
            if (uni == 0xec && isolated) return 0xFD;
            if (uni == 0xec && (initial || medial)) return 0xFE;


            /* farsi yeh - two dots underneath */
            //ed
            if (uni == 0xed && l) return 0xFC;
            if (uni == 0xed && isolated) return 0xFD;
            if (uni == 0xed && (initial || medial)) return 0xFE;


            /* no-break space */
            //a0
            if (uni == 0xA0) return 0xFF;

            //System.out.println("Ne povis konverti: >  " + uni + " " + Integer.toHexString((int)uni));

            return ApplyShaparakProtocol(uni);
        }

        private static byte ApplyShaparakProtocol(byte uni)
        {
            /* Arab numbers / Arabaj ciferoj */
            if (uni >= 0x30 && uni <= 0x39)
                return (byte)(uni + 80);
            if (uni == 0x20)
                return 0xff;
            else //(uni < 0x80)
            {
//                Logger.Log(String.Format("WARN: ASCII character {0}:{1}", uni, (char)uni));
                return 0xff;
            }
        }

        public static byte[] ArabicToIranSys(byte[] szSrc)
        {
            var szTemp = new byte[szSrc.Length];
            var tmpRvsFlag = new bool[szSrc.Length];
            if (szSrc.Length == 1)
            {
                szTemp[0] = Convert1256Iran((byte)' ', szSrc[0], (byte)' ');
            }
            else if (szSrc.Length > 1)
            {
                szTemp[0] = Convert1256Iran((byte)' ', szSrc[0], szSrc[1]);
                tmpRvsFlag[0] = BTxtReverse(szTemp[0], szSrc[0]);
                int i;
                for (i = 1; i < szSrc.Length - 1; i++)
                {
                    szTemp[i] = Convert1256Iran(szSrc[i - 1], szSrc[i], szSrc[i + 1]);
                    tmpRvsFlag[i] = BTxtReverse(szTemp[i], szSrc[i]);
                }
                szTemp[i] = Convert1256Iran(szSrc[i - 1], szSrc[i], (byte)' ');
                tmpRvsFlag[i] = BTxtReverse(szTemp[i], szSrc[i]);

                int stIndex = 0;
                int strIndex = 0;

                while (strIndex < szTemp.Length)
                {
                    while (strIndex < tmpRvsFlag.Length && tmpRvsFlag[strIndex]) strIndex++;

                    int enIndex = strIndex - 1;

                    if (enIndex > stIndex)
                    {
                        // copy the orginal txt
                        var count = enIndex - stIndex + 1;
                        var tmpRvsTxt = new byte[count];
                        Buffer.BlockCopy(szTemp, stIndex, tmpRvsTxt, 0, count);

                        // in place reversal
                        tmpRvsTxt = PubStrReverse(tmpRvsTxt);
                        for (int j = 0; j < tmpRvsTxt.Length; j++)
                            tmpRvsTxt[j] = (byte)(tmpRvsTxt[j] == 0x20 ? 0xFF : tmpRvsTxt[j]);
                        // putting the string back
                        Buffer.BlockCopy(tmpRvsTxt, 0, szTemp, stIndex, count);
                    }

                    // ommiting all the sequence of FALSE;
                    while (strIndex < szTemp.Length && !tmpRvsFlag[strIndex])
                        strIndex++;
                    stIndex = strIndex;
                }
            }
            return szTemp;
        }

        private static bool BTxtReverse(byte a, byte b)
        {
            if (a == b && a != 32)
            {
//                Logger.Log(String.Format("WARN: ASCII character {0}:{1}", b, (char) b));
            }
            if (a == 32)
                return true;
            else if (b >= 0x30 && b <= 0x39)
                return false;
            else
                return a != b;
        }

        private static byte[] PubStrReverse(byte[] pszStringInOut)
        {
            int i, j, iLength;
            byte ucTmp;

            if (pszStringInOut == null)
            {
                return null;
            }

            iLength = pszStringInOut.Length;
            for (i = 0, j = iLength - 1; i < iLength / 2; i++, j--)
            {
                ucTmp = pszStringInOut[i];
                pszStringInOut[i] = pszStringInOut[j];
                pszStringInOut[j] = ucTmp;
            }
            return pszStringInOut;
        }
    }
}