namespace Interfaces
{
    public interface ICallOnSceneChange
    {
        /// <summary>
        /// Méthode appelée lors d'un changement de scène.
        /// </summary>
        /// <param name="objectsIndex">L index de l object dans la liste, pour pouvoir l enlever avec removeAt si on veut le detruire a un changement de scene</param>
        void OnSceneChange(int objectsIndex);
    }
}