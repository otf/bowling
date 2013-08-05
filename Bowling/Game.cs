using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Game
    {
        readonly List<Func<IEnumerable<int>, int>> totalOfPins
            = new List<Func<IEnumerable<int>, int>>();

        int nthOfFrame = 0;
        int totalOfFrame = 0;
        int indexOfFrame = 0;
        const int MaxPins = 10;
        bool waitingForFuture = false;

        bool IsLastOfFrame()
        {
            return nthOfFrame == (IsWaitingForFuture() ? 2 : 1);
        }

        bool IsFinalFrame()
        {
            return indexOfFrame == 9;
        }

        bool IsWaitingForFuture()
        {
            return waitingForFuture && IsFinalFrame();
        }

        public void Roll(int pins)
        {
            totalOfFrame += pins;

            if (totalOfFrame == MaxPins)
            {
                if (!IsLastOfFrame())
                {
                    RollStrike(pins);
                }
                else
                {
                    RollSpare(pins);
                }
            }
            else
            {
                RollJustly(pins);
            }
        }

        public void Roll(params int[] pinss)
        {
            foreach (var pins in pinss) Roll(pins);
        }

        private void NextFrame()
        {
            nthOfFrame = 0;
            totalOfFrame = 0;
            indexOfFrame++;
        }

        private void RollStrike(int pins)
        {
            if (!IsFinalFrame())
            {
                totalOfPins.Add((tail) => pins + tail.First() + tail.ElementAt(1));
                NextFrame();
            }
            else
            {
                totalOfPins.Add((tail) => pins);
            }
            waitingForFuture = true;
        }

        private void RollJustly(int pins)
        {
            totalOfPins.Add((tail) => pins);
            if (!IsLastOfFrame())
            {
                nthOfFrame++;
            }
            else
            {
                NextFrame();
            }

            waitingForFuture = false;
        }

        private void RollSpare(int pins)
        {
            if (!IsFinalFrame())
            {
                totalOfPins.Add((tail) => pins + tail.First());
                NextFrame();
            }
            else
            {
                totalOfPins.Add((tail) => pins);
            }

            waitingForFuture = true;
        }

        static IEnumerable<int> CalculateFuture(IEnumerable<Func<IEnumerable<int>, int>> future)
        {
            return future.Select((pins, idx) => pins(CalculateFuture(future.Skip(idx + 1))));
        }

        public int Score()
        {
            return CalculateFuture(totalOfPins).Sum();
        }

        public IEnumerable<IEnumerable<string>> ScoreSequense()
        {
            throw new NotImplementedException();
        }
    }
}