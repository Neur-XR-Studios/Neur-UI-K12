using System;
using System.Collections.Generic;

namespace Simulanis.ContentSDK.K12.HindiFontConverter
{
    public static class HindiConvert
{
    public static string Convert(string text)
    {
        string Value = text;

        List<string> hindicharacters = new List<string>{"क","ख","ग","घ","ङ","च","छ","ज","झ","ञ","ट","ठ",
            "ड","ढ","ण","त","थ","द","ध","न","ऩ","प","फ","ब","भ","म","य","र","ऱ","ल","ळ","ऴ","व","श","ष",
            "स","ह" };


        List<OCharacters> ocharacters = new List<OCharacters>{
        new OCharacters(){ OCharacter = "को", HindiCharacter = "क",ECharacter="की",AACharacter="का" },
        new OCharacters(){ OCharacter = "खो", HindiCharacter = "ख",ECharacter="खी",AACharacter="खा" },
        new OCharacters(){ OCharacter = "गो", HindiCharacter = "ग",ECharacter="गी",AACharacter="गा" },
        new OCharacters(){ OCharacter = "घो", HindiCharacter = "घ",ECharacter= "घी",AACharacter="घा"},
        new OCharacters(){ OCharacter = "ङो", HindiCharacter = "ङ",ECharacter= "ङी",AACharacter="ङा" },
        new OCharacters(){ OCharacter = "चो", HindiCharacter = "च",ECharacter= "ची",AACharacter="चा" },
        new OCharacters(){ OCharacter = "छो", HindiCharacter = "छ",ECharacter= "छी",AACharacter="छा" },
        new OCharacters(){ OCharacter = "जो", HindiCharacter = "ज",ECharacter= "जी",AACharacter="जा" },
        new OCharacters(){ OCharacter = "झो", HindiCharacter = "झ",ECharacter= "झी",AACharacter="झा" },

        new OCharacters(){ OCharacter = "टो", HindiCharacter = "ट",ECharacter= "टी",AACharacter="टा" },
        new OCharacters(){ OCharacter = "ठो", HindiCharacter = "ठ",ECharacter= "ठी" ,AACharacter="ठा"},
        new OCharacters(){ OCharacter = "डो", HindiCharacter = "ड",ECharacter= "डी",AACharacter="डा" },
        new OCharacters(){ OCharacter = "ढो", HindiCharacter = "ढ" ,ECharacter= "ढी",AACharacter="ढा"},
        new OCharacters(){ OCharacter = "णो", HindiCharacter = "ण",ECharacter= "णी" ,AACharacter="णा"},

        new OCharacters(){ OCharacter = "तो", HindiCharacter = "त",ECharacter= "ती",AACharacter="ता" },
        new OCharacters(){ OCharacter = "थो", HindiCharacter = "थ",ECharacter= "थी",AACharacter="था" },
        new OCharacters(){ OCharacter = "दो", HindiCharacter = "द",ECharacter= "दी" ,AACharacter="दा"},
        new OCharacters(){ OCharacter = "धो", HindiCharacter = "ध" ,ECharacter= "धी",AACharacter="धा"},
        new OCharacters(){ OCharacter = "नो", HindiCharacter = "न" ,ECharacter= "नी",AACharacter="ना"},

        new OCharacters(){ OCharacter = "पो", HindiCharacter = "प",ECharacter= "पी",AACharacter="का" },
        new OCharacters(){ OCharacter = "फो", HindiCharacter = "फ",ECharacter= "फी" ,AACharacter="फा"},
        new OCharacters(){ OCharacter = "बो", HindiCharacter = "ब",ECharacter= "बी" ,AACharacter="बा"},
        new OCharacters(){ OCharacter = "भो", HindiCharacter = "भ",ECharacter= "भी" ,AACharacter="भा"},
        new OCharacters(){ OCharacter = "मो", HindiCharacter = "म",ECharacter= "मी",AACharacter="मा" },
        new OCharacters(){ OCharacter = "यो", HindiCharacter = "य",ECharacter= "यी",AACharacter="या" },
        new OCharacters(){ OCharacter = "लो", HindiCharacter = "ल",ECharacter= "ली",AACharacter="ला" },

        new OCharacters(){ OCharacter = "वो", HindiCharacter = "व",ECharacter= "वी",AACharacter="वा" },
        new OCharacters(){ OCharacter = "शो", HindiCharacter = "श",ECharacter= "शी",AACharacter="शा" },
        new OCharacters(){ OCharacter = "षो", HindiCharacter = "ष",ECharacter= "षी",AACharacter="षा" },
        new OCharacters(){ OCharacter = "सो", HindiCharacter = "स",ECharacter= "सी",AACharacter="सा" },
        new OCharacters(){ OCharacter = "हो", HindiCharacter = "ह",ECharacter= "ही",AACharacter="हा" },
       };

        List<WrongCharacters> wrongCharacters = new List<WrongCharacters>{
        new WrongCharacters(){ character = "क्", unicode = "E783" },
        new WrongCharacters(){ character = "ख्", unicode = "E784" },
        new WrongCharacters(){ character = "ग्", unicode = "E785" },
        new WrongCharacters(){ character = "घ्", unicode = "E786" },
        new WrongCharacters(){ character = "च्", unicode = "E788" },
        new WrongCharacters(){ character = "छ्", unicode = "E789" },
        new WrongCharacters(){ character = "ज्", unicode = "E78A" },
        new WrongCharacters(){ character = "झ्", unicode = "E78B" },
        new WrongCharacters(){ character = "ञ्", unicode = "E78C" },
        new WrongCharacters(){ character = "ण्", unicode = "E791" },
        new WrongCharacters(){ character = "त्", unicode = "E792" },
        new WrongCharacters(){ character = "थ्", unicode = "E793" },
        new WrongCharacters(){ character = "ध्", unicode = "E795" },
        new WrongCharacters(){ character = "न्", unicode = "E796" },
        new WrongCharacters(){ character = "प्", unicode = "E797" },
        new WrongCharacters(){ character = "फ्", unicode = "E798" },
        new WrongCharacters(){ character = "ब्", unicode = "E799" },
        new WrongCharacters(){ character = "भ्", unicode = "E79A" },
        new WrongCharacters(){ character = "म्", unicode = "E79B" },
        new WrongCharacters(){ character = "य्", unicode = "E79C" },
        new WrongCharacters(){ character = "ल्", unicode = "E79E" },
        new WrongCharacters(){ character = "व्", unicode = "E7A0" },
        new WrongCharacters(){ character = "श्", unicode = "E7A1" },
        new WrongCharacters(){ character = "ष्", unicode = "E7A2" },
        new WrongCharacters(){ character = "स्", unicode = "E7A3" },
       };

        List<WrongCharacters> dotWrongCharacters = new List<WrongCharacters>{
        new WrongCharacters(){ character = "क़्", unicode = "E7A9" },
        new WrongCharacters(){ character = "ख़्", unicode = "E7AA" },
        new WrongCharacters(){ character = "ग़्", unicode = "E7AB" },
        new WrongCharacters(){ character = "ज़्", unicode = "E7B0" },
        new WrongCharacters(){ character = "त़्", unicode = "E7B8" },
        new WrongCharacters(){ character = "थ़्", unicode = "E7B9" },
        new WrongCharacters(){ character = "ऩ्", unicode = "E7BC" },
        new WrongCharacters(){ character = "फ़्", unicode = "E7BE" },
        new WrongCharacters(){ character = "य़्", unicode = "E7C2" },
        new WrongCharacters(){ character = "ष़्", unicode = "E7C7" },

       };

        List<WrongCharacters> ircharacters = new List<WrongCharacters>{
        new WrongCharacters(){ character = "क्", unicode = "E7CA" },
        new WrongCharacters(){ character = "ख्", unicode = "E7CB" },
        new WrongCharacters(){ character = "ग्", unicode = "E7CC" },
        new WrongCharacters(){ character = "घ्", unicode = "E7CD" },
        new WrongCharacters(){ character = "च्", unicode = "E7CF" },
        new WrongCharacters(){ character = "छ्", unicode = "E7D0" },
        new WrongCharacters(){ character = "ज्", unicode = "E7D1" },
        new WrongCharacters(){ character = "झ्", unicode = "E7D2" },
        new WrongCharacters(){ character = "ञ्", unicode = "E7D3" },
        new WrongCharacters(){ character = "ट्", unicode = "E7D4" },
        new WrongCharacters(){ character = "ठ्", unicode = "E7D5" },
        new WrongCharacters(){ character = "ङ्", unicode = "E7D6" },
        new WrongCharacters(){ character = "ढ्", unicode = "E7D7" },
        new WrongCharacters(){ character = "ण्", unicode = "E7D8" },

        new WrongCharacters(){ character = "त्", unicode = "E7D9" },
        new WrongCharacters(){ character = "थ्", unicode = "E7DA" },
        new WrongCharacters(){ character = "द्", unicode = "E7DB" },
        new WrongCharacters(){ character = "ध्", unicode = "E7DC" },

        new WrongCharacters(){ character = "प्", unicode = "E7DE" },
        new WrongCharacters(){ character = "फ्", unicode = "E7DF" },
        new WrongCharacters(){ character = "ब्", unicode = "E7E0" },
        new WrongCharacters(){ character = "भ्", unicode = "E7E1" },
        new WrongCharacters(){ character = "म्", unicode = "E7E2" },
        new WrongCharacters(){ character = "य्", unicode = "E7E3" },

        new WrongCharacters(){ character = "व्", unicode = "E7E7" },
        new WrongCharacters(){ character = "श्", unicode = "E7E8" },
        new WrongCharacters(){ character = "ष्", unicode = "E7E9" },
        new WrongCharacters(){ character = "स्", unicode = "E7EA" },
        new WrongCharacters(){ character = "ह्", unicode = "E7EB" },
       };

        int appendValueE = System.Convert.ToInt32("093F", 16);
        string AppendStringE = System.Convert.ToChar(appendValueE).ToString();


        int appendValueM = System.Convert.ToInt32("E925", 16);
        string AppendStringM = System.Convert.ToChar(appendValueM).ToString();

        int appendValueIR = System.Convert.ToInt32("E781", 16);
        string AppendStringIR = System.Convert.ToChar(appendValueIR).ToString();

        int appendValueET = System.Convert.ToInt32("E922", 16);
        string AppendStringET = System.Convert.ToChar(appendValueET).ToString();


        if (Value.Contains("श्चि"))
        {
            int appendValue = System.Convert.ToInt32("E8F8", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("श्चि", AppendStringM + AppendString);

        }
        if (Value.Contains("श्च"))
        {
            int appendValue = System.Convert.ToInt32("E8F8", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("श्च", AppendString);
        }
        if (Value.Contains("श्न"))
        {
            int appendValue = System.Convert.ToInt32("E8FB", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("श्न", AppendString);
        }

        if (Value.Contains("ह्म"))
        {
            int appendValue = System.Convert.ToInt32("E90A", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ह्म", AppendString);
        }
        if (Value.Contains("ह्") && Value.Contains("य"))
        {
            int appendVal = System.Convert.ToInt32("E90B", 16);
            string AppendString = System.Convert.ToChar(appendVal).ToString();
            Value = Value.Replace("ह्" + "य", AppendString);
        }
        if (Value.Contains("हृ"))
        {
            int appendValue = System.Convert.ToInt32("E861", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("हृ", AppendString);
        }

        if (Value.Contains("न्न"))
        {
            int appendValue = System.Convert.ToInt32("E8EB", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("न्न", AppendString);
        }
        if (Value.Contains("प्न"))
        {
            int appendValue = System.Convert.ToInt32("E8ED", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("प्न", AppendString);
        }
        if (Value.Contains("ह्न"))
        {
            int appendValue = System.Convert.ToInt32("E909", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ह्न", AppendString);
        }

        if (Value.Contains("ह्ल"))
        {
            int appendValue = System.Convert.ToInt32("E90C", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ह्ल", AppendString);
        }
        if (Value.Contains("ह्व"))
        {
            int appendValue = System.Convert.ToInt32("E90D", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ह्व", AppendString);
        }
        if (Value.Contains("र्") && Value.Contains("ज्") && Value.Contains("ञ"))
        {

            int appendValue = System.Convert.ToInt32("E780", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("र्" + "ज्" + "ञ", AppendString + AppendStringIR);

        }

        if (Value.Contains("ज्ञ"))
        {
            int appendValue = System.Convert.ToInt32("E780", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ज्ञ", AppendString);
        }

        if (Value.Contains("द्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8E2", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("द्" + "य", AppendString);
        }
        if (Value.Contains("ड्") && Value.Contains("ढ"))
        {
            int appendValue = System.Convert.ToInt32("E8CC", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ड्" + "ढ", AppendString);
        }



        if (Value.Contains("क्षि"))
        {
            int appendValue = System.Convert.ToInt32("E77F", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("क्षि", AppendStringM + AppendString);
        }


        if (Value.Contains("र्") && Value.Contains("श्") && Value.Contains("व"))
        {
            int appendValue = System.Convert.ToInt32("E8F9", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();


            if (Value.Contains(AppendStringE))
            {

                Value = Value.Replace("र्" + "श्" + "व" + AppendStringE, AppendStringE + AppendString + AppendStringIR);
            }

            Value = Value.Replace("र्" + "श्" + "व", AppendString + AppendStringIR);

        }

        if (Value.Contains("श्व"))
        {
            int appendValue = System.Convert.ToInt32("E8F9", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("श्व", AppendString);
        }
        if (Value.Contains("द्") && Value.Contains("व"))
        {
            int appendValue = System.Convert.ToInt32("E8DB", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("द्" + "व" + AppendStringE, AppendStringE + AppendString);
            }
            Value = Value.Replace("द्" + "व", AppendString);

        }

        if (Value.Contains("त्") && Value.Contains("त"))
        {

            int appendValue = System.Convert.ToInt32("E8D3", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("त्" + "त" + AppendStringE, AppendStringM + AppendString);
            }
                Value = Value.Replace("त्" + "त", AppendString);

        }
        if (Value.Contains("द्") && Value.Contains("ब"))
        {

            int appendValue = System.Convert.ToInt32("E8D9", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("द्" + "ब", AppendString);

        }

        if (Value.Contains("द्") && Value.Contains("म"))
        {

            int appendValue = System.Convert.ToInt32("E8E1", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("द्" + "म", AppendString);

        }
        if (Value.Contains("र्") && Value.Contains("क्") && Value.Contains("ष"))
        {
            int AppendValue = System.Convert.ToInt32("E77F", 16);
            string AppendString = System.Convert.ToChar(AppendValue).ToString();

            Value = Value.Replace("र्" + "क्" + "ष", AppendString + AppendStringIR);

        }



        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < ircharacters.Count; j++)
            {

                if (Value.Contains(ircharacters[j].character) && Value.Contains(ircharacters[j].character) && Value.Contains(AppendStringE) && Value.Contains("र"))
                {
                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    int AppendValue2 = System.Convert.ToInt32(ircharacters[j].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                    Value = Value.Replace(wrongCharacters[i].character + ircharacters[j].character + "र" + AppendStringE, AppendStringET + AppendString + AppendString2);
                }
                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(ircharacters[j].character) && Value.Contains("र"))
                {

                    int appendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                    int appendValue3 = System.Convert.ToInt32(ircharacters[j].unicode, 16);
                    string AppendString3 = System.Convert.ToChar(appendValue3).ToString();


                    Value = Value.Replace(wrongCharacters[i].character + ircharacters[j].character + "र", AppendString2 + AppendString3);

                }

            }
        }

        for (int i = 0; i < hindicharacters.Count; i++)
        {

            if (Value.Contains("क्") && Value.Contains("ष्") && Value.Contains(hindicharacters[i]))
            {
                int AppendValue2 = System.Convert.ToInt32("E7A7", 16);
                string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                if (Value.Contains(AppendStringE))
                {
                    Value = Value.Replace("क्" + "ष्" + hindicharacters[i] + AppendStringE, AppendStringET + AppendString2 + hindicharacters[i]);
                }
                Value = Value.Replace("क्" + "ष्" + hindicharacters[i], AppendString2 + hindicharacters[i]);
            }

        }

        if (Value.Contains("क्") && Value.Contains("ष"))
        {
            int AppendValue = System.Convert.ToInt32("E77F", 16);
            string AppendString = System.Convert.ToChar(AppendValue).ToString();

            Value = Value.Replace("क्" + "ष", AppendString);

        }


        for (int i = 0; i < ircharacters.Count; i++)
        {
            if (Value.Contains("र्") && Value.Contains(ircharacters[i].character) && Value.Contains("रो"))
            {

                int appendValue = System.Convert.ToInt32(ircharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                int appendValue2 = System.Convert.ToInt32("094B", 16);
                string AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                Value = Value.Replace("र्" + ircharacters[i].character + "रो", AppendString + AppendString2 + AppendStringIR);

            }
            if (Value.Contains(ircharacters[i].character) && Value.Contains(AppendStringE) && Value.Contains("र"))
            {
                int AppendValue = System.Convert.ToInt32(ircharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(AppendValue).ToString();
                Value = Value.Replace(ircharacters[i].character + "र" + AppendStringE, AppendStringM + AppendString);
            }
            if (Value.Contains("र्") && Value.Contains(ircharacters[i].character) && Value.Contains("र"))
            {

                int appendValue = System.Convert.ToInt32(ircharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                if (Value.Contains(AppendStringE))
                {
                    Value = Value.Replace(ircharacters[i].character + "र" + AppendStringE, AppendStringE + AppendString);
                }

                Value = Value.Replace("र्" + ircharacters[i].character + "र", AppendString + AppendStringIR);
            }

            if (Value.Contains(ircharacters[i].character) && Value.Contains("रु"))
            {
                int appendValue = System.Convert.ToInt32(ircharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                int appendValue2 = System.Convert.ToInt32("0941", 16);
                string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                Value = Value.Replace(ircharacters[i].character + "रु", AppendString + AppendString2);
            }

            if (Value.Contains(ircharacters[i].character) && Value.Contains("र"))
            {
                int AppendValue = System.Convert.ToInt32(ircharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(AppendValue).ToString();
                Value = Value.Replace(ircharacters[i].character + "र", AppendString);
            }

        }


        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < ocharacters.Count; j++)
            {
                if (Value.Contains("र्") && Value.Contains(wrongCharacters[i].character) && Value.Contains(ocharacters[j].OCharacter))
                {
                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    int appendValue2 = System.Convert.ToInt32("E898", 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

                    Value = Value.Replace("र्" + wrongCharacters[i].character + ocharacters[j].OCharacter, AppendString + ocharacters[j].HindiCharacter + AppendString2);
                }
            }
        }


        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < wrongCharacters.Count; j++)
            {
                for (int k = 0; k < hindicharacters.Count; k++)
                {
                    if (Value.Contains(wrongCharacters[i].character) && Value.Contains(wrongCharacters[j].character))
                    {
                        int AppendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                        string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                        int AppendValue3 = System.Convert.ToInt32(wrongCharacters[j].unicode, 16);
                        string AppendString3 = System.Convert.ToChar(AppendValue3).ToString();

                        string oldvalue = wrongCharacters[i].character + wrongCharacters[j].character + hindicharacters[k] + AppendStringE;
                        Value = Value.Replace(oldvalue, AppendStringET + AppendString2 + AppendString3 + hindicharacters[k]);

                    }
                    else if (Value.Contains(wrongCharacters[i].character) && !Value.Contains(wrongCharacters[j].character) && Value.Contains(hindicharacters[k]) && Value.Contains(AppendStringE)) //Value.Contains(AppendStringE) && Value.Contains(hindicharacters[j]))
                    {
                        int AppendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                        string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                        Value = Value.Replace(wrongCharacters[i].character + hindicharacters[k] + AppendStringE, AppendStringET + AppendString2 + hindicharacters[k]);

                    }

                    else if (Value.Contains(wrongCharacters[i].character) && Value.Contains(wrongCharacters[j].character) && !Value.Contains(hindicharacters[k]))
                    {
                        int AppendValue2 = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                        string AppendString2 = System.Convert.ToChar(AppendValue2).ToString();

                        int AppendValue3 = System.Convert.ToInt32(wrongCharacters[j].unicode, 16);
                        string AppendString3 = System.Convert.ToChar(AppendValue3).ToString();

                        string oldvalue = wrongCharacters[i].character + wrongCharacters[j].character;
                        Value = Value.Replace(oldvalue, AppendString2 + AppendString3);
                    }

                }
            }
        }
        for (int i = 0; i < dotWrongCharacters.Count; i++)
        {
            for (int j = 0; j < hindicharacters.Count; j++)
            {
                if (Value.Contains(dotWrongCharacters[i].character) && Value.Contains(hindicharacters[j]) && Value.Contains(AppendStringE)) //Value.Contains(AppendStringE) && Value.Contains(hindicharacters[j]))
                {
                    int AppendValue = System.Convert.ToInt32(dotWrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    Value = Value.Replace(dotWrongCharacters[i].character + hindicharacters[j] + AppendStringE, AppendStringET + AppendString + hindicharacters[j]);

                }

                if (Value.Contains(dotWrongCharacters[i].character) && Value.Contains(hindicharacters[j]))
                {
                    int AppendValue = System.Convert.ToInt32(dotWrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    string oldvalue = dotWrongCharacters[i].character + hindicharacters[j];
                    Value = Value.Replace(oldvalue, AppendString + hindicharacters[j]);
                }
            }
        }


        for (int i = 0; i < ocharacters.Count; i++)
        {
            if (Value.Contains("र्") && Value.Contains(ocharacters[i].OCharacter))
            {

                int appendValue = System.Convert.ToInt32("E899", 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();
                Value = Value.Replace("र्" + ocharacters[i].OCharacter, ocharacters[i].HindiCharacter + AppendString);
            }
            if (Value.Contains("र्") && Value.Contains(ocharacters[i].ECharacter))
            {
                int appendValue = System.Convert.ToInt32("0940", 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                Value = Value.Replace("र्" + ocharacters[i].ECharacter, ocharacters[i].HindiCharacter + AppendString + AppendStringIR);

            }
            for (int j = 0; j < wrongCharacters.Count; j++)
            {
                if (Value.Contains("र्") && Value.Contains(wrongCharacters[j].character) && Value.Contains(ocharacters[i].AACharacter))
                {
                    int appendValue = System.Convert.ToInt32(wrongCharacters[j].unicode, 16);
                    string AppendString = System.Convert.ToChar(appendValue).ToString();

                    Value = Value.Replace("र्" + wrongCharacters[j].character + ocharacters[i].AACharacter, AppendString + ocharacters[i].AACharacter + AppendStringIR);
                }
            }

            if (Value.Contains("र्") && Value.Contains(ocharacters[i].AACharacter))
            {
                Value = Value.Replace("र्" + ocharacters[i].AACharacter, ocharacters[i].AACharacter + AppendStringIR);
            }

        }


        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < hindicharacters.Count; j++)
            {
                if (Value.Contains("र्") && Value.Contains(wrongCharacters[i].character) && Value.Contains(hindicharacters[j]))
                {
                    int appendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(appendValue).ToString();
                    Value = Value.Replace("र्" + wrongCharacters[i].character + hindicharacters[j], AppendString + hindicharacters[j] + AppendStringIR);
                }
            }

        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < hindicharacters.Count; j++)
            {
                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(hindicharacters[j]))
                {
                    int AppendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(AppendValue).ToString();

                    Value = Value.Replace(wrongCharacters[i].character + hindicharacters[j], AppendString + hindicharacters[j]);

                }
            }
        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            for (int j = 0; j < ircharacters.Count; j++)
            {
                if (Value.Contains(wrongCharacters[i].character) && Value.Contains(ircharacters[j].character) && Value.Contains("र"))
                {
                    int appendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                    string AppendString = System.Convert.ToChar(appendValue).ToString();

                    int appendValue2 = System.Convert.ToInt32(ircharacters[j].unicode, 16);
                    string AppendString2 = System.Convert.ToChar(appendValue2).ToString();
                    Value = Value.Replace(wrongCharacters[i].character + ircharacters[j].character + "र", AppendString + AppendString2);
                }

            }

        }

        for (int i = 0; i < hindicharacters.Count; i++)
        {
                int appendValue = System.Convert.ToInt32("E8A0", 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();

                int appendValue2 = System.Convert.ToInt32("0902", 16);
                string AppendString2 = System.Convert.ToChar(appendValue2).ToString();
            if (Value.Contains("र्") && Value.Contains(AppendStringE) && Value.Contains(hindicharacters[i]))
            {
                Value = Value.Replace("र्" + hindicharacters[i] + AppendStringE, AppendStringM + hindicharacters[i] + AppendStringIR);
            }
            if (Value.Contains("र्") && Value.Contains(hindicharacters[i]) && Value.Contains(AppendString2))
            {
               Value = Value.Replace("र्" + hindicharacters[i] + AppendString2, hindicharacters[i] + AppendString);
            }
            if (Value.Contains("र्") && Value.Contains(hindicharacters[i]))
            {
                Value = Value.Replace("र्" + hindicharacters[i], hindicharacters[i] + AppendStringIR);
            }
        }

        for (int i = 0; i < wrongCharacters.Count; i++)
        {
            if (Value.Contains(wrongCharacters[i].character))
            {
                int appendValue = System.Convert.ToInt32(wrongCharacters[i].unicode, 16);
                string AppendString = System.Convert.ToChar(appendValue).ToString();
                Value = Value.Replace(wrongCharacters[i].character, AppendString);
            }
        }

        if (Value.Contains("श्") && Value.Contains("रु"))
        {
            int appendValue = System.Convert.ToInt32("E7E8", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            int appendValue2 = System.Convert.ToInt32("0941", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            Value = Value.Replace("श्" + "रु", AppendString + AppendString2);

        }

        if (Value.Contains("रु"))
        {
            int appendValue = System.Convert.ToInt32("E86B", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("रु", AppendString);

        }
        if (Value.Contains("रू"))
        {
            int appendValue = System.Convert.ToInt32("E86C", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("रू", AppendString);

        }

        if (Value.Contains("त्") && Value.Contains("त"))
        {
            int appendValue = System.Convert.ToInt32("E8D3", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace(wrongCharacters[9].character + hindicharacters[13], AppendString);

        }
        if (Value.Contains("क्") && Value.Contains("ष्"))
        {
            int appendValue = System.Convert.ToInt32("E7A7", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("क्" + "ष्", AppendString);

        }
        if (Value.Contains("क्ष"))
        {
            int appendValue = System.Convert.ToInt32("E77F", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("क्ष", AppendString);
        }
        if (Value.Contains("क्") && Value.Contains("ष"))
        {
            int appendValue = System.Convert.ToInt32("E77F", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("क्" + "ष", AppendString);

        }
        if (Value.Contains("द्") && Value.Contains("ध"))
        {
            int appendValue2 = System.Convert.ToInt32("E8D7", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("द्" + "ध" + AppendStringE, AppendStringM + AppendString2);
            }
            Value = Value.Replace("द्" + "ध", AppendString2);
        }


        if (Value.Contains("ट्") && Value.Contains("ट"))
        {
            int appendValue2 = System.Convert.ToInt32("E8C5", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("ट्" + "ट" + AppendStringE, AppendStringM + AppendString2);
            }
            Value = Value.Replace("ट्" + "ट", AppendString2);

        }

        if (Value.Contains("ट्") && Value.Contains("ठ"))
        {
            int appendValue2 = System.Convert.ToInt32("E8C7", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("ट्" + "ठ" + AppendStringE, AppendStringM + AppendString2);
            }
            Value = Value.Replace("ट्" + "ठ", AppendString2);

        }
        if (Value.Contains("ठ्") && Value.Contains("ठ"))
        {
            int appendValue2 = System.Convert.ToInt32("E8CA", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("ठ्" + "ठ" + AppendStringE, AppendStringM + AppendString2);
            }
            Value = Value.Replace("ठ्" + "ठ", AppendString2);
        }
        if (Value.Contains("ठ्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8CB", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ठ्" + "य", AppendString);
        }
        if (Value.Contains("ठ्"))
        {
            Value = Value.Replace("ठ्", "ठ");
        }
        if (Value.Contains("ट्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8C9", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();


            Value = Value.Replace("ट्" + "य", AppendString);
        }
        if (Value.Contains("ड्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8CF", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ड्" + "य", AppendString);
        }
        if (Value.Contains("ड्") && Value.Contains("ड"))
        {
            int appendValue = System.Convert.ToInt32("E8CD", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ड्" + "ड", AppendString);
        }

        if (Value.Contains("छ्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8C1", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("छ्" + "य", AppendString);
        }
        if (Value.Contains("ड्") && Value.Contains("ड"))
        {
            int appendValue = System.Convert.ToInt32("E8CD", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ढ्" + "य", AppendString);
        }
        if (Value.Contains("ढ्") && Value.Contains("ढ"))
        {
            int appendValue = System.Convert.ToInt32("E8D1", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ढ्" + "ढ", AppendString);
        }
        if (Value.Contains("ढ्") && Value.Contains("य"))
        {
            int appendValue = System.Convert.ToInt32("E8D2", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ढ्" + "य", AppendString);
        }
        if (Value.Contains("ङ्") && Value.Contains("ग"))
        {
            int appendValue = System.Convert.ToInt32("E8BB", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ङ्" + "ग", AppendString);
        }
        if (Value.Contains("ङ्") && Value.Contains("म"))
        {
            int appendValue = System.Convert.ToInt32("E8BC", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();

            Value = Value.Replace("ङ्" + "म", AppendString);
        }

        if (Value.Contains("द्") && Value.Contains("भ"))
        {
            int appendValue2 = System.Convert.ToInt32("E8DA", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();

            if (Value.Contains(AppendStringE))
            {
                Value = Value.Replace("द्" + "भ" + AppendStringE, AppendStringM + AppendString2);
            }
            Value = Value.Replace("द्" + "भ", AppendString2);

        }

        if (Value.Contains("द्") && Value.Contains("द"))
        {
            int appendValue = System.Convert.ToInt32("E8DF", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("द्" + "द", AppendString);
        }
        if (Value.Contains("ड्") && Value.Contains("र"))
        {
            int appendValue = System.Convert.ToInt32("E7D6", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("ड्" + "र", AppendString);
        }
        if (Value.Contains("फ़") && Value.Contains(AppendStringE))
        {
            int appendValue = System.Convert.ToInt32("E72E", 16);
            string AppendString = System.Convert.ToChar(appendValue).ToString();
            Value = Value.Replace("फ़" + AppendStringE, AppendStringM + AppendString);
        }
        if (Value.Contains("ढ़") && Value.Contains(AppendStringE))
        {

            Value = Value.Replace("ढ़" + AppendStringE, AppendStringM + "ढ़");
        }
        if (Value.Contains("श्"))
        {

            int appendValue2 = System.Convert.ToInt32("E7A1", 16);
            string AppendString2 = System.Convert.ToChar(appendValue2).ToString();
            Value = Value.Replace("श्", AppendString2);

        }


        return Value;

        
    }

}

[Serializable]
public class WrongCharacters
{
    public string character;
    public string unicode;
}

[Serializable]
public class OCharacters
{
    public string OCharacter;
    public string HindiCharacter;
    public string ECharacter;
    public string AACharacter;
}
}
