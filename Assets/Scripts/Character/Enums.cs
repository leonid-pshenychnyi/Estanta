namespace Character
{
    public enum CameraPositionType
    {
        FirstPersonView = 0,
        ThirdPersonView = 1,
        Free = 2
    }

    public enum Ports
    {
        // Characters + 1
        Characters = 732,
        UCharacters = 733,
    
        // Environment + 1
        Environment = 734,
        UEnvironment = 735,
    
        // Npc + 1
        Npc = 736,
        UNpc = 737,
    
        Users = 738
    }
}