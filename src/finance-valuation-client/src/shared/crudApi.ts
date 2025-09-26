export async function fetchEntries(endpoint: string) {
  const apiUrl = process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${endpoint}`, {
    method: "GET",
    credentials: "include",
  })
  if (!response.ok) {
    throw new Error(`Failed to fetch ${endpoint}`);
  }
  return response.json();
}

export async function createOrUpdate(endpoint: string, body: any) {
  const {id, ...rest} = body;
  if (id !== undefined) {
    return await update(endpoint, body.id, {...rest})
  }
  return await create(endpoint, {...rest});
}

export async function create(endpoint: string, body: {}) {
  const apiUrl = process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${endpoint}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error(`Failed to create ${endpoint}`);
  }
}

export async function update(endpoint: string, id: number, body: {}) {
  const apiUrl = process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${endpoint}/${id}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error(`Failed to update ${endpoint}`);
  }
}

export async function deleteEntry(endpoint: string, id: number) {
  const apiUrl = process.env.REACT_APP_API_URL || '';
  const response = await fetch(`${apiUrl}/${endpoint}/${id}`, {
    method: 'DELETE',
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error(`Failed to delete ${endpoint}`);
  }
}