namespace DataGridVueDotnet
{
  /// <summary>
  /// Request data interface sent by the data grid to get the current grid state.
  /// <see href="https://datagridvue.com/generated/classes/ServerSideStorageService.html">Server Side Storage Service</see>
  /// </summary>
  /// <typeparam name="TUserId">The type of the user identifier.</typeparam>
  public class GetGridStateRequest<TUserId>
  {
    /// <summary>
    /// The unique identifier for the current user.
    /// </summary>
    public TUserId? UserId { get; set; }
  }
}
