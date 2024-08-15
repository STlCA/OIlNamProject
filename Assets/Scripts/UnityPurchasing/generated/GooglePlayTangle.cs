// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("7PL8L3sJrA9OmU4D/jmqJ2+rmnVfC4NjSfFQQ7j036dBA2VzcTLr65qYpOpvi6jeTjWtASP9pc7Vk1GKVtXb1ORW1d7WVtXV1BRUzC3xMy2Etjcp/ydG/poMBZKMlJW84zeiMeRW1fbk2dLd/lKcUiPZ1dXV0dTXtVmK2WgWGZrilt0qJ4/eDQ7IU7tPK8+PgL8uz0fJoqmkOyXWmc3GW68LfV9cOknWrJ390+54B7Hf14EnqDdHM6MqD67r/fH6p+b6E9jpSuMsP1TofXYcLMc4BSclT6nW5CiXbBEFp3afwqc5FKomsmXd3kN5IgQUH6Ed1SCjaNxIiZNJvhyXv92wf38XY3ML/5l8PJeqqQ/jf6WVD1TFNLGj3XReoip3S9bX1dTV");
        private static int[] order = new int[] { 6,5,12,5,11,6,11,7,8,10,10,11,12,13,14 };
        private static int key = 212;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
