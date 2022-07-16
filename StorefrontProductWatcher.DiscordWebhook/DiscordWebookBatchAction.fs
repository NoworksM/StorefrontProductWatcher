namespace StorefrontProductWatcher.DiscordWebhook

open System.Net.Http
open System.Net.Http.Json
open System.Threading.Tasks
open Microsoft.Extensions.Logging
open StorefrontProductWatcher.DiscordWebhook
open StorefrontProductWatcher.Sdk.Data
open StorefrontProductWatcher.Sdk.Interfaces

module WebhookAction =
    let WebhookUrlKey = "url"

    let BuildEmbedForProduct (product: Product) =
        { Author = None
          Title = Some(product.Name)
          Description = None
          Color = None
          Url = Some(product.Url)
          Fields =
              Some(
                  [ { Name = "Price"
                      Value = $"${product.Price}"
                      Inline = Some(true) } ]
              )
          Image = Some({ Url = product.ImageUrl })
          Thumbnail = None
          Footer = None }

    type DiscordWebhookBatchProductAction(logger: ILogger<DiscordWebhookBatchProductAction>, httpClient: HttpClient) =
        member this.logger = logger
        member this.httpClient = httpClient

        interface IBatchProductAction with
            member this.ExecuteAsync(storefront, products, config) =
                let found, rawWebhookUrl = config.TryGetValue WebhookUrlKey

                if not found || not <| rawWebhookUrl :? string then
                    this.logger.LogError "Invalid config: No value specified for \"webhookUrl\""
                    Task.CompletedTask
                else
                    let message =
                        { Username = None
                          AvatarUrl = None
                          Content = Some $"New products are available on {storefront}"
                          Embeds = Some(products |> Seq.map BuildEmbedForProduct) }

                    task {
                        let! result = this.httpClient.PostAsync(rawWebhookUrl :?> string, JsonContent.Create(message))

                        if not <| result.IsSuccessStatusCode then
                            this.logger.LogError ""
                    }
