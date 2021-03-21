import todoService from "../app/todoService.js";

new Vue({
    // options
    el: '#app',
    data: {
        todos: [],
        todo: {
            title: ""
        }
    },
    created() {
        this.getTodos();
    },
    methods: {
        addTodo() {
        },
        async getTodos() {
            
            var todos = await todoService
                .getItems();

                this.todos = [];
                todos.forEach(doc => {
                    this.todos.push(new { title: doc});
                });
        },
        updateTodoItem(docId, e) {
            var isChecked = e.target.checked;
            firebase
                .firestore()
                .collection("users")
                .doc(firebase.auth().currentUser.uid)
                .collection("todos")
                .doc(docId)
                .update({
                    isCompleted: isChecked
                });
        },
        deleteToDo(docId) {
            firebase
                .firestore()
                .collection("users")
                .doc(firebase.auth().currentUser.uid)
                .collection("todos")
                .doc(docId)
                .delete();
        }
    }
});
