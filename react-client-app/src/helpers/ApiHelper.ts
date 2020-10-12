

export function api<T>(url: string): Promise<T> {
    return fetch(url)
      .then(response => {

        if (response.status != 200) {
          throw new Error(response.statusText)
        }else{
        return response.json().then(data => data as T)
        };
      })          
}