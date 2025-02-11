﻿using System;
using System.Drawing;
using FieldEffect.Interfaces;

namespace FieldEffect.Models
{
    public class BatteryIcon : IBatteryIcon
    {
        protected bool _isDisposed;

        public enum BatteryOrientation
        {
            /// <summary>
            /// Battery is oriented horizontally, leftmost value is zero.
            /// </summary>
            HorizontalL,

            /// <summary>
            /// Battery is oriented vertically, bottom value is zero.
            /// </summary>
            VerticalB,

            /// <summary>
            /// Battery is oriented horizontally, with rightmost value as zero.
            /// </summary>
            HorizontalR,

            /// <summary>
            /// Battery is oriented vertically, with topmost value as zero.
            /// </summary>
            VerticalT
        }

        private readonly Icon _batteryTemplate;
        private Icon _renderedBattery;
        private Rectangle _batteryLevelMask;
        private readonly BatteryOrientation _batteryOrientation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="batteryTemplate">An icon containing the main artwork for the battery</param>
        /// <param name="batteryLevelMask">A rectangle on the template that we will draw the battery level in</param>
        /// <param name="batteryOrientation">The orientation of the battery icon</param>
        public BatteryIcon(Icon batteryTemplate, Rectangle batteryLevelMask, BatteryOrientation batteryOrientation)
        {
            _batteryTemplate = batteryTemplate;
            _batteryLevelMask = batteryLevelMask;

            //Copy the icon
            _renderedBattery = Icon.FromHandle(batteryTemplate.Handle);
            _batteryOrientation = batteryOrientation;
        }

        /// <summary>
        /// Battery level, from 0 - 100
        /// </summary>
        public int BatteryLevel { get; set; }

        public Icon RenderedIcon => _renderedBattery;

        public void Render()
        {
            using var template = _batteryTemplate.ToBitmap();
            using var g = Graphics.FromImage(template);
            var batteryRect = _batteryOrientation switch
            {
                BatteryOrientation.HorizontalL => new Rectangle(_batteryLevelMask.X, _batteryLevelMask.Y,
                    (int) (_batteryLevelMask.Width * (BatteryLevel / 100.0)), _batteryLevelMask.Height),
                _ => throw new NotImplementedException("Only BatteryOrientation.HorizontalL is currently implemented.")
            };

            g.Clear(Color.Transparent);
            g.DrawIcon(_batteryTemplate, 0, 0);
            using var brush = new SolidBrush(BatteryLevel <= 10 ? Color.Red : Color.White);
            g.FillRectangle(brush, batteryRect);

            if (_renderedBattery != null)
            {
                _renderedBattery.Dispose();
                var hIcon = template.GetHicon();
                _renderedBattery = Icon.FromHandle(hIcon);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {
                    _renderedBattery?.Dispose();
                    _isDisposed = true;
                }
            }
        }
    }
}
