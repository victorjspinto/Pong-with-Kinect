using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;
using System.Threading;

namespace TutorialPong
{
    class Kinect
    {
        private const int MAX_FRAME_RATE = 5;

        Runtime kinect;

        public Kinect()
        {
            kinect = new Runtime();

            //inicializa
            kinect.Initialize(RuntimeOptions.UseDepthAndPlayerIndex | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);

            kinect.DepthStream.Open(ImageStreamType.Depth, 2, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);
            kinect.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);
            kinect.SkeletonEngine.TransformSmooth = true;

            kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
        }

        DateTime lastFrame = DateTime.Now;
        float LastHead = 0.0f;
        private void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            LastHead = 0.0f;
            foreach (SkeletonData data in e.SkeletonFrame.Skeletons)
            {
                if (data.TrackingState == SkeletonTrackingState.Tracked)
                {
                    Vector positionHandLeft = data.Joints[JointID.HandLeft].Position;
                    Vector positionShoulderLeft = data.Joints[JointID.ShoulderLeft].Position;
                    Vector positionHandRight = data.Joints[JointID.HandRight].Position;
                    Vector positionShoulderRigth = data.Joints[JointID.ShoulderRight].Position;

                    double result = positionHandLeft.Y - positionHandRight.Y;

                    if (result > 0.1)
                    {
                        Side = KinectSide.Rigth;
                    }
                    else if (result < -0.1)
                    {
                        Side = KinectSide.Left;
                    }
                    else
                    {
                        Side = KinectSide.Center;
                    }

                    if (LastHead == 0.0f)
                    {
                        LastHead = data.Joints[JointID.Head].Position.X;
                        
                        result = positionShoulderLeft.Y - positionHandLeft.Y + 0.10f;
                        result *= 7.5f * 100;
                        if (result < 0) result = 0;
                        if (result > 510) result = 510;
                        PosicaoLeft = (int)Math.Truncate(result);

                        result = positionShoulderRigth.Y - positionHandRight.Y + 0.10f;
                        result *= 7.5f * 100;
                        if (result < 0) result = 0;
                        if (result > 510) result = 510;
                        PosicaoRigth = (int)Math.Truncate(result);
                    }
                    else
                    {
                        if (LastHead < data.Joints[JointID.Head].Position.X)
                        {
                            result = positionShoulderRigth.Y - positionHandRight.Y + 0.10f;
                            result *= 7.5f * 100;
                            if (result < 0) result = 0;
                            if (result > 510) result = 510;
                            PosicaoRigth = (int)Math.Truncate(result);
                        }
                        else
                        {
                            result = positionShoulderLeft.Y - positionHandLeft.Y + 0.10f;
                            result *= 7.5f * 100;
                            if (result < 0) result = 0;
                            if (result > 510) result = 510;
                            PosicaoLeft = (int)Math.Truncate(result);
                        }
                    }
                }
            }
        }

        public KinectSide Side = KinectSide.Center;

        public Int32 PosicaoRigth = 0;

        public Int32 PosicaoLeft = 0;
    }

    public enum KinectSide
    {
        Left,
        Rigth,
        Center
    }
}
