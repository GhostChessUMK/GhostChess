using GhostChess.RaspberryPi;
using System;
using System.Collections.Generic;
using System.Text;
using static GhostChess.RaspberryPi.Enums;

namespace RPiSingalR_ReedDemo
{
    public class Setup
    {
        public Gpio[] ReedMatrix_Out { get; } = new Gpio[2];
        public Gpio[] ReedMatrix_In { get; } = new Gpio[2];

        public void Init()
        {
            Gpio gpio6 = new Gpio(Pins.Gpio6, InputType.Input);
            Gpio gpio13 = new Gpio(Pins.Gpio13, InputType.Input);
            Gpio gpio19 = new Gpio(Pins.Gpio19, InputType.Output);
            Gpio gpio26 = new Gpio(Pins.Gpio26, InputType.Output);

            ReedMatrix_Out[0] = gpio6;
            ReedMatrix_Out[1] = gpio13;
            ReedMatrix_In[0] = gpio19;
            ReedMatrix_In[1] = gpio26;
        }

        public bool[,] GetOutput()
        {
            bool[,] output = new bool[2, 2];
            ReadOut(0);
            output = Scan(0, output);
            ReadOut(1);
            output = Scan(1, output);
            return output;
        }

        public void ReadOut(int input)
        {
            ReedMatrix_In[input].SetState(State.High);
            ReedMatrix_Out[0].GetState();
            ReedMatrix_Out[1].GetState();
            ReedMatrix_In[input].SetState(State.Low);
        }

        public bool[,] Scan(int input, bool[,] output)
        {
            if (ReedMatrix_Out[0].Value == State.High)
                output[input, 0] = true;
            else
                output[input, 0] = false;

            if (ReedMatrix_Out[1].Value == State.High)
                output[input, 1] = true;
            else
                output[input, 1] = false;

            return output;
        }
    }
}
