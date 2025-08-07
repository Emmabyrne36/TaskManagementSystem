import json
import requests
import time

API_URL = "http://taskmanager-api:5000/api/tasks"

def wait_for_api():
    for _ in range(10):
        try:
            requests.get(API_URL)
            break
        except Exception:
            time.sleep(2)
    else:
        print("API not available.")
        exit(1)

def should_seed():
    try:
        resp = requests.get(API_URL)
        if resp.status_code == 200:
            data = resp.json()
            return len(data.get('items', [])) == 0
    except Exception as e:
        print(f"Error checking existing tasks: {e}")
    return False

def seed_database():
    wait_for_api()
    if should_seed():
        print("Seeding database...")
        with open("seed.json") as f:
            tasks = json.load(f)
        for task in tasks:
            resp = requests.post(API_URL, json=task)
            print(f"Seeding {task['title']}: {resp.status_code}")
    else:
        print("Tasks already exist. Skipping seeding.")

seed_database()
print("Database seeding Completed.")