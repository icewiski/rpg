/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class ReflectionUtils
{
    public static System.Type[] GetAllSubtypes(System.Type baseType, bool fromAllAssemblies)
    {
        var types = GetAllTypes(fromAllAssemblies);
        return (from System.Type type in types where type.IsSubclassOf(baseType) select type).ToArray();
    }

    public static System.Type[] GetAllTypes(bool fromAllAssemblies)
    {
        var assemblies = new List<Assembly>();
        assemblies.Add(System.Reflection.Assembly.GetExecutingAssembly());
        if (fromAllAssemblies)
        {
            foreach (var assemblyName in System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                var assembly = System.Reflection.Assembly.Load(assemblyName.ToString());
                assemblies.Add(assembly);
            }
        }

        var types = new List<System.Type>();
        foreach (var assembly in assemblies)
        {
            types.AddRange(assembly.GetTypes());
        }
        return types.ToArray();
    }

}
