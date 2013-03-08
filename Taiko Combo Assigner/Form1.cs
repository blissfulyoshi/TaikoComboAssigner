using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taiko_Combo_Assigner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //using the hitsound and combo number, identify what type of drumhit was used
        public int typeOfDrumHit(int combo, int hitsound)
        {
            //if spinner
            if (combo % ComboMultiplier == SpinnerVariation)
                return spinner;
            //if hitcircle
            if (combo % ComboMultiplier == ComboVariation || combo == SameComboCircle)
            {
                switch (hitsound)
                {
                    case none:
                        return red;
                    case whistle:
                        return blue;
                    case finish:
                        return bigRed;
                    case clap:
                        return blue;
                    case whistleFinish:
                        return bigBlue;
                    case whistleClap:
                        return blue;
                    case clapFinish:
                        return bigBlue;
                    case whistleClapFinish:
                        return bigBlue;
                }
            }
            //if slider (you only care if the slider tick has a finish or not)
            if (combo % ComboMultiplier == ComboVariation + SliderVariation || combo == SameComboCircle + SliderVariation)
            {
                switch (hitsound)
                {
                    case finish:
                        return finishSlider;
                    case whistleFinish:
                        return finishSlider;
                    case clapFinish:
                        return finishSlider;
                    case whistleClapFinish:
                        return finishSlider;
                    default:
                        return slider;
                }
            }
            return -1; //the program should not run into the error case under standard circumstances
        }

        //returns the number that says what combo color the note should be
        public string findComboModifier(int drumHit, int currentCombo, int maxColors)
        {
            //beat is a spinner
            if (drumHit == spinner)
                return (SpinnerVariation).ToString();
            //beat is a drumhit, not a slider or spinner
            if (drumHit > currentCombo && drumHit < slider)
                return (ComboMultiplier * (drumHit - currentCombo) - 11).ToString();
            if (drumHit < currentCombo && drumHit < slider)
                return (ComboMultiplier * (maxColors - currentCombo + drumHit) - 11).ToString();
            if (drumHit == currentCombo && drumHit < slider)
                return (SameComboCircle).ToString();
            //beat is a slider
            if (drumHit > currentCombo && drumHit >= slider)
                return (ComboMultiplier * (drumHit - currentCombo) - 11 + SliderVariation).ToString();
            if (drumHit < currentCombo && drumHit >= slider)
                return (ComboMultiplier * (maxColors - currentCombo + drumHit) - 11 + SliderVariation).ToString();
            if (drumHit == currentCombo && drumHit >= slider)
                return (SameComboCircle + SliderVariation).ToString();
            return "-1"; //no idea what to do in the case of the error
        }

        //checks if the next note is the same combo color as the previous note
        //if true, then have the spinner act as a new combo that adds 1 combo
        //not efficient, but since case is rare, it shouldn't be too bad
        public string findSpinnerCombo(ref int currentCombo, int maxColors, string beatmapComponenet)
        {
            string[] hitobjectInformation = beatmapComponenet.Split(',');
            int drumHit = typeOfDrumHit(Convert.ToInt32(hitobjectInformation[comboSlot]), Convert.ToInt32(hitobjectInformation[hitSoundSlot]));
            if (drumHit == currentCombo)
            {
                currentCombo = (currentCombo + 1) % maxColors;
                return (SpinnerVariation + ComboMultiplier).ToString();
            }
            return (SpinnerVariation).ToString();
        }

        //If you click the process button, the program will create a version of the .osu file where each type of drumhit in taiko has its own hitsound
        //currently, there is an assumption that there are going to be 6 combo colors
        private void Process_Click(object sender, EventArgs e)
        {
            bool hitObject = false;
            int currentCombo = 0;
            int maxColors = 6; //number of combo colors
            this.OutputText.Text = ""; //clean up output
            string[] lines = InputText.Text.Split('\n');
            int lineCount = lines.Count();
            //process the hit objects while copying all the other lines
            for (int i = 0; i < lineCount; i++)
            {
                string beatmapComponenet = lines[i];
                if (hitObject)
                {
                    //split the hitObject by commas and find out what type of taiko note it is
                    string[] hitobjectInformation = beatmapComponenet.Split(',');
                    int drumHit = typeOfDrumHit(Convert.ToInt32(hitobjectInformation[comboSlot]), Convert.ToInt32(hitobjectInformation[hitSoundSlot]));
                    //find what combo belongs there and update the currentCombo (remember to ignore spinner when calculating combos)
                    hitobjectInformation[comboSlot] = findComboModifier(drumHit, currentCombo, maxColors);
                    if (drumHit != spinner)
                        currentCombo = drumHit;
                    //since spinners do not play a part in the current combo unless the notes on both side of the spinner need to be the same color, spinners have their own form of processing
                    else if (drumHit == spinner && (i + 1) < lineCount) 
                    {
                        hitobjectInformation[comboSlot] = findSpinnerCombo(ref currentCombo, maxColors, lines[i + 1]);
                    }

                    //add text
                    string newHitobjectInformation = String.Join(",", hitobjectInformation);
                    OutputText.Text += newHitobjectInformation + "\n";
                }
                //copy the lines that are not hitobjects
                else
                    OutputText.Text += beatmapComponenet + "\n";
                //find where teh hitobjects are
                if (beatmapComponenet.Contains("[HitObjects]"))
                {
                    hitObject = true;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
