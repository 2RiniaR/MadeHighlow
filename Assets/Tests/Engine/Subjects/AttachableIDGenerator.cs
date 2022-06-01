using Moq;

namespace RineaR.MadeHighlow
{
    public static class AttachableIDGenerator
    {
        public static IAttachableID Fake(ID content)
        {
            var stubParentID = new Mock<IAttachableID>();
            stubParentID.SetupGet(id => id.Content).Returns(content);
            return stubParentID.Object;
        }

        public static IAttachableID Empty => Fake(ID.None);
    }
}
