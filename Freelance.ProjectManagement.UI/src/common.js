export const  dataGridOnRowUpdating = (options) => {
    options.newData = {...options.oldData, ...options.newData};
}