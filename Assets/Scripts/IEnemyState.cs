public interface IEnemyState
{
    void EnterState(EnemyController enemy); // Hàm gọi khi vào trạng thái
    void UpdateState(EnemyController enemy); // Hàm gọi mỗi frame
    void ExitState(EnemyController enemy); // Hàm gọi khi rời trạng thái
}
