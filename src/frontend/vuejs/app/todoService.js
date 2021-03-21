export default {
    getItems: async function () {
        var result = await fetch("/backend/todo",
            {
                method: "GET",
                headers: {
                    Accept: 'application/json',
                }
            });
        return await result.json();
    }
}