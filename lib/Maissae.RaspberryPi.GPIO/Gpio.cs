using System;
using static GhostChess.RaspberryPi.Enums;

namespace GhostChess.RaspberryPi
{
    public class Gpio
    {
        public Pins Pin { get; private set; }
        public bool IsExported { get; private set; } = false;
        public bool IsDirected { get; private set; } = false;
        public InputType InputType { get; private set; }
        public State Value { get; private set; }

        public Gpio(Pins pin, bool export = false)
        {
            Pin = pin;
            if(export)
            {
                Export();
            }
        }

        public Gpio(Pins pin, InputType inputType)
        {
            Pin = pin;
            Clean();
            Export();
            SetDirection(inputType);
        }

        public Gpio(Pins pin, InputType inputType, State state)
        {
            Pin = pin;
            Clean();
            Export();
            SetDirection(inputType);
            SetState(state);
        }

        public void Export()
        {
            if (IsExported == false)
            {
                ShellHelper.Execute($"sudo sh -c \"echo '{(int)Pin}' > /sys/class/gpio/export\"");
                IsExported = true;
            }
            else
            {
                Console.WriteLine($"Pin {(int)Pin} is already active!");
            }
        }

        public void Unexport()
        {
            if (IsExported)
            {
                ShellHelper.Execute($"sudo sh -c \"echo '{(int)Pin}' > /sys/class/gpio/unexport\"");
                IsExported = false;
            }
            else
            {
                Console.WriteLine($"Pin {(int)Pin} is not active!");
            }
        }

        public void SetState(State value)
        {
            if(IsDirected)
            {
                ShellHelper.Execute($"sudo sh -c \"echo '{(int)value}' > /sys/class/gpio/gpio{(int)Pin}/value\"");
            }
            else
            {
                Console.WriteLine($"Pin {(int)Pin} doesn't have direction set up!");
            }
        }

        public void GetState()
        {
            var value = ShellHelper.Execute($"sudo sh -c \"cat /sys/class/gpio/gpio{(int)Pin}/value\"");
            int _value = int.Parse(value);
            if (_value == 1)
            {
                Value = State.High;
            }
            else
            {
                Value = State.Low;
            }
        }

        public void SetDirection(InputType inputType)
        {
            if (inputType == InputType.Output)
            {
                ShellHelper.Execute($"sudo sh -c \"echo \"out\" > /sys/class/gpio/gpio{(int)Pin}/direction\"");
            }
            else if (inputType == InputType.Input)
            {
                ShellHelper.Execute($"sudo sh -c \"echo \"in\" > /sys/class/gpio/gpio{(int)Pin}/direction\"");
            }
            IsDirected = true;
        }

        public void Clean()
        {
            ShellHelper.Execute($"sudo sh -c \"echo '{(int)Pin}' > /sys/class/gpio/unexport\"");
        }
    }
}
