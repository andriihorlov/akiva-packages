using Mirror;
using System;
using System.Collections.Generic;

namespace SpecialNeeds.Network
{
    [Serializable]
    public struct StartSessionMessage : NetworkMessage
    {
        public int userId;
        public int caseId;
        public int environmentId;
        public int[] avatarsIDs;
        public int musicId;
        public int[] awardsIds;
        public int localizationId;
        public string phoneAppVersion;
        public int gameVariant;

        public StartSessionMessage(int userId, int caseId, int environmentId, int[] avatarIds, int musicId, int[] awardsIds, int localizationId, string version, int gameVariant)
        {
            this.userId = userId;
            this.caseId = caseId;
            this.environmentId = environmentId;
            this.avatarsIDs = avatarIds;
            this.musicId = musicId;
            this.awardsIds = awardsIds;
            this.localizationId = localizationId;
            this.phoneAppVersion = version;
            this.gameVariant = gameVariant;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.WriteInt(userId);
            writer.WriteInt(caseId);
            writer.WriteInt(environmentId);
            writer.WriteInt(avatarsIDs.Length);

            foreach (var id in avatarsIDs)
            {
                writer.WriteInt(id);
            }

            writer.WriteInt(musicId);

            writer.WriteInt(awardsIds.Length);

            foreach (var id in awardsIds)
            {
                writer.WriteInt(id);
            }
            
            writer.WriteInt(localizationId);
            writer.WriteString(phoneAppVersion);
            writer.WriteInt(gameVariant);
        }

        public void Deserialize(NetworkReader reader)
        {
            userId = reader.ReadInt();
            caseId = reader.ReadInt();
            environmentId = reader.ReadInt();

            avatarsIDs = new int[reader.ReadInt()];

            for (int i = 0; i < avatarsIDs.Length; i++)
            {
                avatarsIDs[i] = reader.ReadInt();
            }

            musicId = reader.ReadInt();

            awardsIds = new int[reader.ReadInt()];

            for (int i = 0; i < awardsIds.Length; i++)
            {
                awardsIds[i] = reader.ReadInt();
            }

            localizationId = reader.ReadInt();
            phoneAppVersion = reader.ReadString();
            gameVariant = reader.ReadInt();
        }
    }

    public struct ReadyToStartMessage : NetworkMessage
    {
        public string version;

        public ReadyToStartMessage(string version)
        {
            this.version = version;
        }
        public void Serialize(NetworkWriter writer)
        {
            writer.WriteString(version);
        }

        public void Deserialize(NetworkReader reader)
        {
            version = reader.ReadString();
        }
    }

    public struct SessionStartedMessage : NetworkMessage { }

    public struct TerminateSessionMessage : NetworkMessage { }

    public struct SessionEndedMessage : NetworkMessage { }

    public struct SessionProgressMessage : NetworkMessage
    {
         public int avatarId;
         //public int hitUseCount;
         public int awardGetCount;
         public int awardAllCount;
         public float focus;

         public string stepId;
         public bool isPart;
         public string stepPartId;
         public int partsCount;
         public int stepCount;
         public int hintUseCount;
         public int hintAllCount;
         public bool stepStatus;

         public SessionProgressMessage(int avatarId, int awardGetCount, 
             int awardAllCount, float focus, string stepId,bool isPart,string stepPartId,int partsCount, int stepCount, int hintUseCount, int hintAllCount, bool stepStatus)
         {
             this.avatarId = avatarId;
             this.awardGetCount = awardGetCount;
             this.awardAllCount = awardAllCount;
             this.focus = focus;

             this.stepId = stepId;
             this.isPart = isPart;
             this.stepPartId = stepPartId;
             this.partsCount = partsCount;
             this.stepCount = stepCount;
             this.hintUseCount = hintUseCount;
             this.hintAllCount = hintAllCount;
             this.stepStatus = stepStatus;
         }
                  
         public void Serialize(NetworkWriter writer)
         {
             writer.WriteInt(avatarId);
             
             writer.WriteInt(awardGetCount);
             writer.WriteInt(awardAllCount);

             writer.WriteFloat(focus);
         }

         public void Deserialize(NetworkReader reader)
         {
             avatarId = reader.ReadInt();
             
             awardGetCount = reader.ReadInt();
             awardAllCount = reader.ReadInt();

             focus = reader.ReadFloat();
         }
    }

    public struct SessionDataSendingResultMessage : NetworkMessage
    {
        public SessionDataSendingResultMessage(bool success)
        {
            this.success = success;
        }

        public bool success;

        public  void Deserialize(NetworkReader reader)
        {
            success = reader.ReadBool();
        }

        public  void Serialize(NetworkWriter writer)
        {
            writer.WriteBool(success);
        }
    }

    public struct DisconnectClientMessage : NetworkMessage
    {
        public string name;

        public DisconnectClientMessage(string name)
        {
            this.name = name;
        }

        public  void Serialize(NetworkWriter writer)
        {
            writer.WriteString(name);
        }

        public  void Deserialize(NetworkReader reader)
        {
            name = reader.ReadString();
        }
    }

    public struct StartUpdateMessage : NetworkMessage { }

    public struct UpdateProgressMessage : NetworkMessage 
    {
        public float progress;

        public UpdateProgressMessage(float _progress)
        {
            progress = _progress;
        }
    }

    public struct FinishUpdateMessage : NetworkMessage { }

    public struct ErrorUpdateMessage : NetworkMessage
    {
        public string description;

        public ErrorUpdateMessage(string description)
        {
            this.description = description;
        }
    }
    
    public struct CancelUpdateMessage : NetworkMessage { }

    public struct SessionStepsMessage : NetworkMessage
    {
        public int stepCount;

        public SessionStepsMessage(int stepCount)
        {
            this.stepCount = stepCount;
        }
        public void Serialize(NetworkWriter writer)
        {
            writer.WriteInt(stepCount);
        }
        public void Deserialize(NetworkReader reader)
        {
            stepCount = reader.ReadInt();
        }
    }
}