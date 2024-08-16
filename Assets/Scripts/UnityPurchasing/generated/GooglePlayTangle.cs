// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("9Uv3P8pJgjaiY3mjVPZ9VTdalZUGGBbFkeNG5aRzpOkU00DNhUFwn1+zYDOC/PNwCHw3wM1lNOfkIrlRQt2t2UnA5UQBFxsQTQwQ+TIDoAkOvD8cDjM4NxS4drjJMz8/Pzs+PaXBJWVqVcQlrSNIQ07RzzxzJyyxteFpiaMbuqlSHjVNq+mPmZvYAQFuXN3DFc2sFHDm73hmfn9WCd1I20Xhl7W20KM8RncXOQSS7Vs1PWvN++9NnHUoTdP+QMxYjzc0qZPI7v79iZnhFXOW1n1AQ+UJlU9/5b4v3sbVvgKXnPbGLdLvzc+lQzwOwn2GvD8xPg68PzQ8vD8/Pv6+Jscb2cdwck4AhWFCNKTfR+vJF08kP3m7YFtJN560SMCdoTw9Pz4/");
        private static int[] order = new int[] { 2,6,4,10,4,7,7,11,8,10,13,13,13,13,14 };
        private static int key = 62;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
