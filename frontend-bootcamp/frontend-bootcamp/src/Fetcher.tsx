async function fetcher<T>(url: string, method: string = "GET", body?: any): Promise<T> {
  if (body === undefined) {
    return fetch(url, {method: method})
      .then(response => response.json())
  } else {
    return fetch(url, {
      method: method,
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(body)
    })
      .then(response => response.json())
  }
}
export default fetcher;