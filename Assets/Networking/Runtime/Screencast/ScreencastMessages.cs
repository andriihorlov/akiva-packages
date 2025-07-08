using Mirror;

namespace SpecialNeeds.Network.Screencast
{
    public struct StartScreencastMessage : NetworkMessage { }

    public struct StopScreencastMessage : NetworkMessage { }

    public struct ScreencastMessage : NetworkMessage
    {
        public byte[] bytes;
        public int width;
        public int height;

        public  void Deserialize(NetworkReader reader)
        {
            width = reader.ReadInt();
            height = reader.ReadInt();
            bytes = reader.ReadBytesAndSize();
        }

        public  void Serialize(NetworkWriter writer)
        {
            writer.WriteInt(width);
            writer.WriteInt(height);
            writer.WriteBytesAndSize(bytes);
        }
    }
}