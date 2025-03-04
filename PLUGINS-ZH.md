<h1 align="center">
���ϵͳ
</h1>
<p align="center">
TerraAngel ֧��ͨ�������ʽ������չ
</p>
<br>

# ��������

## �������

TerraAngel ���Զ�ʶ�� `Plugins` �ļ����������� `.TAPlugin.dll` ��β�� .NET �����Ϊ�����

�����������ʱ��TerraAngel ���ڲ�������в��Ҽ̳��� `TerraAngel.PluginAPI` �ж���� `Plugin` ��ľ���ʵ�֡�

## ʾ�����

����һ����� .NET 7.0 �� .NET �����Ŀ�������� .TAPlugin ��β������ Example.TAPlugin��

��ͨ�������д�����`dotnet new classlib --name Example.TAPlugin --framework net7.0`

```cs
using TerraAngel;
using TerraAngel.Plugin;

namespace Example.TAPlugin;

public class ExamplePlugin : Plugin
{
    public override string Name => "ʾ�����";

    public ExamplePlugin(string path) : base(path)
    {

    }

    // �������ʱ����һ��
    public override void Load()
    {

    }

    // ���ж��ʱ����һ��
    public override void Unload()
    {

    }

    // ��������ڼ�ÿ֡����
    public override void Update()
    {

    }
}
```

��Ҫ��Ӷ����³��򼯵����ã�
- `TerraAngelPluginAPI.dll`
- `Terraria.dll`

����ͬʱ���ã�
 - `ReLogic.dll`
 - `ImGui.NET.dll`
 - `TNA.dll`

��Щ�ļ�λ�� TerraAngel �Ĺ���Ŀ¼�У�`TerraAngel/Terraria/bin/Release/net7.0/`��

��װ���ʱ������� DLL ���Ƶ� TerraAngel �Ĳ��Ŀ¼���ɡ�

# ����ʾ��

## ��ӿ���̨����

```cs
using TerraAngel;
using TerraAngel.Plugin;
using Terraria.DataStructures;

namespace Console.TAPlugin;

public class ConsoleExamplePlugin : Plugin
{
    public override string Name => "����̨ʾ�����";

    public ConsoleExample(string path) : base(path)
    {

    }

    // �������ʱ����
    public override void Load()
    {
        // �����̨�������
        ClientLoader.Console.AddCommand("kill_player",
            (x) =>
            {
                Main.LocalPlayer.KillMe(PlayerDeathReason.ByPlayer(Main.myPlayer), 1, 0);
            },
            "ɱ�����");
    }

    // ���ж��ʱ����
    public override void Unload()
    {

    }

    // ÿ֡����
    public override void Update()
    {
        
    }
}
```