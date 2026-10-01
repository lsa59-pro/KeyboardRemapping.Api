namespace KeyboardRemapping.Api.Data;

/// <summary>
/// Définit une touche avec son nom et son code HID
/// </summary>
/// <param name="Name"></param>
/// <param name="HidUsageCode"></param>
public record HidKeyDefinition(string Name, int HidUsageCode);

/// <summary>
/// Liste de touches et de leurs codes HID standards
/// [USB HID Usage Tables specification] - (Usage Page 0x07 — Keyboard/Keypad).
/// <see cref="https://usb.org/sites/default/files/hut1_5.pdf) "/>>
/// (Constantes)
/// </summary>
public static class HidKeyCatalog
{
    /// <summary>
    /// Liste des touches disponibles avec leurs codes HID
    /// </summary>
    public static IReadOnlyList<HidKeyDefinition> All { get; } =
    [
        // Lettres
        new("A", 0x04), // 4
        new("B", 0x05), // 5
        new("C", 0x06), // 6
        new("D", 0x07), // 7
        new("E", 0x08), // 8
        new("F", 0x09), // 9
        new("G", 0x0A), // 10
        new("H", 0x0B), // 11
        new("I", 0x0C), // 12
        new("J", 0x0D), // 13
        new("K", 0x0E), // 14
        new("L", 0x0F), // 15
        new("M", 0x10), // 16
        new("N", 0x11), // 17
        new("O", 0x12), // 18
        new("P", 0x13), // 19
        new("Q", 0x14), // 20
        new("R", 0x15), // 21
        new("S", 0x16), // 22
        new("T", 0x17), // 23
        new("U", 0x18), // 24
        new("V", 0x19), // 25
        new("W", 0x1A), // 26
        new("X", 0x1B), // 27
        new("Y", 0x1C), // 28
        new("Z", 0x1D), // 29

        // Nombres
        new("1", 0x1E), // 30
        new("2", 0x1F), // 31
        new("3", 0x20), // 32
        new("4", 0x21), // 33
        new("5", 0x22), // 34
        new("6", 0x23), // 35
        new("7", 0x24), // 36
        new("8", 0x25), // 37
        new("9", 0x26), // 38
        new("0", 0x27), // 39

        // Caracteres Speciaux 
        new("Enter", 0x28),         // 40
        new("Escape", 0x29),        // 41
        new("Backspace", 0x2A),     // 42
        new("Tab", 0x2B),           // 43
        new("Space", 0x2C),         // 44
        new("Caps Lock", 0x39),     // 57
        new("Delete", 0x4C),        // 76
        new("Insert", 0x49),        // 73
        new("Home", 0x4A),          // 74
        new("End", 0x4D),           // 77    
        new("Page Up", 0x4B),       // 75
        new("Page Down", 0x4E),     // 78
        new("Print Screen", 0x46),  // 70
        new("Scroll Lock", 0x47),   // 71
        new("Pause", 0x48),         // 72

        // Touches de fonction
        new("F1", 0x3A),    // 58
        new("F2", 0x3B),    // 59
        new("F3", 0x3C),    // 60
        new("F4", 0x3D),    // 61
        new("F5", 0x3E),    // 62
        new("F6", 0x3F),    // 63
        new("F7", 0x40),    // 64
        new("F8", 0x41),    // 65
        new("F9", 0x42),    // 66
        new("F10", 0x43),   // 67
        new("F11", 0x44),   // 68
        new("F12", 0x45),   // 69

        // Fleches directives
        new("Right Arrow", 0x4F),   // 79
        new("Left Arrow", 0x50),    // 80
        new("Down Arrow", 0x51),    // 81
        new("Up Arrow", 0x52),      // 82

        // Touches de controles
        new("Left Ctrl", 0xE0),             // 224
        new("Left Shift", 0xE1),            // 225
        new("Left Alt", 0xE2),              // 226
        new("Left GUI (Win/Cmd)", 0xE3),    // 227
        new("Right Ctrl", 0xE4),            // 228
        new("Right Shift", 0xE5),           // 229
        new("Right Alt", 0xE6),             // 230
        new("Right GUI (Win/Cmd)", 0xE7),   // 231

        // Pavé numérique
        new("Num Lock", 0x53),      // 83
        new("Numpad /", 0x54),      // 84
        new("Numpad *", 0x55),      // 85
        new("Numpad -", 0x56),      // 86
        new("Numpad +", 0x57),      // 87
        new("Numpad Enter", 0x58),  // 88
        new("Numpad 1", 0x59),      // 89
        new("Numpad 2", 0x5A),      // 90
        new("Numpad 3", 0x5B),      // 91
        new("Numpad 4", 0x5C),      // 92
        new("Numpad 5", 0x5D),      // 93
        new("Numpad 6", 0x5E),      // 94
        new("Numpad 7", 0x5F),      // 95
        new("Numpad 8", 0x60),      // 96
        new("Numpad 9", 0x61),      // 97
        new("Numpad 0", 0x62),      // 98
        new("Numpad .", 0x63)       // 99
    ];
}