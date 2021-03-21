export default {
    getItems: async function() {
        var result = await fetch("https://stvansolano-codebox-dotnet-mc6r-5001.githubpreview.dev/api/v1/todo",
            {
                method: "GET",
                headers: {
                    Accept: 'application/json',
                }
            });
        return await result.json();
    }
}