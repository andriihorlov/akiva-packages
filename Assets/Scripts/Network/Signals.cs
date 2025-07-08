using Mirror;

namespace SpecialNeeds.Signals
{
    public struct StartSignalMessage : NetworkMessage
    {
        public void Deserialize(NetworkReader reader) { }
        public void Serialize(NetworkWriter writer) { }
    }

    public struct StopSignalMessage : NetworkMessage
    {
        public void Deserialize(NetworkReader reader) { }
        public void Serialize(NetworkWriter writer) { }
    }

    public struct StartScreenCastMessage : NetworkMessage
    {
        public void Deserialize(NetworkReader reader) { }
        public void Serialize(NetworkWriter writer) { }
    }

    public struct StopScreenCastMessage : NetworkMessage
    {
        public void Deserialize(NetworkReader reader) { }
        public void Serialize(NetworkWriter writer) { }
    }

    public struct ScreenCastMessage : NetworkMessage
    {
        public byte[] bytes;
        public int width;
        public int height;

        public void Deserialize(NetworkReader reader)
        {
            width = reader.ReadInt();
            height = reader.ReadInt();
            bytes = reader.ReadBytesAndSize();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.WriteInt(width);
            writer.WriteInt(height);
            writer.WriteBytesAndSize(bytes);
        }
    }
}