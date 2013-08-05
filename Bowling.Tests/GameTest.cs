using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void 二回投げて合計値を計算できる()
        {
            var g = new Game();
            g.Roll(3);
            g.Roll(2);

            Assert.That(g.Score(), Is.EqualTo(5));
        }

        [Test]
        public void 三回投げて合計値を計算できる()
        {
            var g = new Game();
            g.Roll(3);
            g.Roll(2);
            g.Roll(5);

            Assert.That(g.Score(), Is.EqualTo(10));
        }

        [Test]
        public void 最初でスペアしたときの合計値を計算できる()
        {
            var g = new Game();
            g.Roll(7);
            g.Roll(3);
            g.Roll(5);

            Assert.That(g.Score(), Is.EqualTo(20));
        }

        [Test]
        public void 最初でストライクしたときの合計値を計算できる()
        {
            var g = new Game();
            g.Roll(10); // 18
            g.Roll(3);  // 8
            g.Roll(5); 

            Assert.That(g.Score(), Is.EqualTo(26));
        }

        [Test]
        public void 最後まで適当に投げる()
        {
            var g = new Game();
            // frame 1
            g.Roll(3); 
            g.Roll(5);

            // frame 2
            g.Roll(1);
            g.Roll(1);

            // frame 3
            g.Roll(2);
            g.Roll(3);

            // frame 4 Spear!
            g.Roll(5);
            g.Roll(5);

            // frame 5
            g.Roll(2);
            g.Roll(3);

            // frame 6 Strike!
            g.Roll(10);

            // frame 7
            g.Roll(2);
            g.Roll(3);

            // frame 8
            g.Roll(1);
            g.Roll(1);

            // frame 9
            g.Roll(3);
            g.Roll(0);

            // frame 10
            g.Roll(3);
            g.Roll(3);

            Assert.That(g.Score(), Is.EqualTo(63));
        }


        [Test]
        public void 最後まで適当に投げる_最後は満点()
        {
            var g = new Game();
            // frame 1
            g.Roll(3);
            g.Roll(5);

            // frame 2
            g.Roll(1);
            g.Roll(1);

            // frame 3
            g.Roll(2);
            g.Roll(3);

            // frame 4 Spear!
            g.Roll(5);
            g.Roll(5);

            // frame 5
            g.Roll(2);
            g.Roll(3);

            // frame 6 Strike!
            g.Roll(10);

            // frame 7
            g.Roll(2);
            g.Roll(3);

            // frame 8
            g.Roll(1);
            g.Roll(1);

            // frame 9
            g.Roll(3);
            g.Roll(0);

            // frame 10 all strike
            g.Roll(10);
            g.Roll(10);
            g.Roll(10);

            Assert.That(g.Score(), Is.EqualTo(87));
        }


        [Test]
        public void 最後まで適当に投げる_最後はスペア()
        {
            var g = new Game();
            // frame 1
            g.Roll(3);
            g.Roll(5);

            // frame 2
            g.Roll(1);
            g.Roll(1);

            // frame 3
            g.Roll(2);
            g.Roll(3);

            // frame 4 Spear!
            g.Roll(5);
            g.Roll(5);

            // frame 5
            g.Roll(2);
            g.Roll(3);

            // frame 6 Strike!
            g.Roll(10);

            // frame 7
            g.Roll(2);
            g.Roll(3);

            // frame 8
            g.Roll(1);
            g.Roll(1);

            // frame 9
            g.Roll(3);
            g.Roll(0);

            // frame 10 all strike
            g.Roll(5);
            g.Roll(5);
            g.Roll(5);

            Assert.That(g.Score(), Is.EqualTo(72));
        }

        [Test]
        public void 入力を複数回受け付ける()
        {
            var g = new Game();
            g.Roll(3, 5, 1, 1, 2, 3, 5, 5, 2, 3, 10, 2, 3, 1, 1, 3, 0, 5, 5, 5);
        
            Assert.That(g.Score(), Is.EqualTo(72));
        }
    }
}
